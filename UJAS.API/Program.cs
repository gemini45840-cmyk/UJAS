using System.Text;
using System.Text.Json;
using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using UJAS.API.Filters;
using UJAS.API.Middleware;
using UJAS.API.Services;
using UJAS.Application;
using UJAS.Infrastructure;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/ujas-api-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Starting UJAS API...");

    // Add services to the container
    ConfigureServices(builder);

    var app = builder.Build();

    // Configure the HTTP request pipeline
    ConfigureMiddleware(app);

    // Initialize database
    await InitializeDatabaseAsync(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

void ConfigureServices(WebApplicationBuilder webBuilder)
{
    var services = webBuilder.Services;
    var configuration = webBuilder.Configuration;

    // Add ProblemDetails
    services.AddProblemDetails(options =>
    {
        options.IncludeExceptionDetails = (ctx, ex) =>
            webBuilder.Environment.IsDevelopment() || webBuilder.Environment.IsStaging();

        options.Map<ArgumentException>(ex => new ProblemDetails
        {
            Title = "Invalid argument",
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message
        });

        options.Map<UnauthorizedAccessException>(ex => new ProblemDetails
        {
            Title = "Unauthorized",
            Status = StatusCodes.Status401Unauthorized,
            Detail = ex.Message
        });
    });

    // Add API versioning
    services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("x-api-version"),
            new UrlSegmentApiVersionReader());
    });

    services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // Add CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

        options.AddPolicy("WidgetPolicy", builder =>
        {
            builder.WithOrigins(configuration["AllowedOrigins:Widgets"]?.Split(',') ?? Array.Empty<string>())
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
    });

    // Add MVC and JSON options
    services.AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
        options.Filters.Add<ValidationFilter>();
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = webBuilder.Environment.IsDevelopment();
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

    // Add rate limiting
    services.AddMemoryCache();
    services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
    services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
    services.AddInMemoryRateLimiting();
    services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

    // Add health checks
    services.AddHealthChecks()
        .AddDbContextCheck<ApplicationDbContext>()
        .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
        .AddRedis(configuration.GetConnectionString("Redis"))
        .AddUrlGroup(new Uri(configuration["ExternalServices:SendGrid"]), "SendGrid API");

    services.AddHealthChecksUI(setup =>
    {
        setup.AddHealthCheckEndpoint("API", "/health");
        setup.AddHealthCheckEndpoint("Database", "/health/database");
    })
    .AddInMemoryStorage();

    // Add Infrastructure and Application layers
    services.AddInfrastructureServices(configuration);
    services.AddApplication();

    // Configure JWT Authentication
    var jwtSettings = configuration.GetSection("Jwt");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = !webBuilder.Environment.IsDevelopment();
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                Log.Warning("Forbidden access attempt to {Path}", context.Request.Path);
                return Task.CompletedTask;
            }
        };
    });

    // Configure Authorization
    services.AddAuthorization(options =>
    {
        // System Administrator policies
        options.AddPolicy("SystemAdminOnly", policy =>
            policy.RequireRole("SystemAdministrator"));

        // Company Administrator policies
        options.AddPolicy("CompanyAdminOnly", policy =>
            policy.RequireRole("CompanyAdministrator"));

        options.AddPolicy("CompanyAdminOrSystemAdmin", policy =>
            policy.RequireRole("CompanyAdministrator", "SystemAdministrator"));

        // Regional Manager policies
        options.AddPolicy("RegionalManagerOnly", policy =>
            policy.RequireRole("RegionalManager"));

        options.AddPolicy("RegionalManagerOrAbove", policy =>
            policy.RequireRole("RegionalManager", "CompanyAdministrator", "SystemAdministrator"));

        // Manager policies
        options.AddPolicy("ManagerOnly", policy =>
            policy.RequireRole("Manager"));

        options.AddPolicy("ManagerOrAbove", policy =>
            policy.RequireRole("Manager", "RegionalManager", "CompanyAdministrator", "SystemAdministrator"));

        // Applicant policies
        options.AddPolicy("ApplicantOnly", policy =>
            policy.RequireRole("Applicant"));

        // Company access policy
        options.AddPolicy("CompanyAccess", policy =>
            policy.Requirements.Add(new CompanyAccessRequirement()));
    });

    // Add HttpContext accessor
    services.AddHttpContextAccessor();

    // Add custom services
    services.AddScoped<ICompanyAccessService, CompanyAccessService>();
    services.AddScoped<IApiKeyService, ApiKeyService>();

    // Add Swagger/OpenAPI
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    services.AddSwaggerGen(options =>
    {
        options.EnableAnnotations();
        options.OperationFilter<SwaggerDefaultValues>();
        options.SchemaFilter<SwaggerExcludeFilter>();

        // Add JWT Authentication support in Swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        // Add API Key support
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
        {
            Description = "API Key authentication",
            Name = "X-API-Key",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        // Include XML comments
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    });

    // Add Feature Management
    services.AddFeatureManagement();

    // Add Response Compression
    services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
        options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
    });

    // Add Memory Cache
    services.AddMemoryCache();

    // Add Distributed Cache (Redis)
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration.GetConnectionString("Redis");
        options.InstanceName = "UJAS_";
    });

    // Add Hosted Services
    services.AddHostedService<BackgroundJobService>();
    services.AddHostedService<DataRetentionService>();

    // Add Application Insights if configured
    if (!string.IsNullOrEmpty(configuration["ApplicationInsights:ConnectionString"]))
    {
        services.AddApplicationInsightsTelemetry(options =>
        {
            options.ConnectionString = configuration["ApplicationInsights:ConnectionString"];
        });
    }
}

void ConfigureMiddleware(WebApplication app)
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Use ProblemDetails middleware
    app.UseProblemDetails();

    // Use CORS
    app.UseCors("AllowAll");

    // Use Rate Limiting
    app.UseIpRateLimiting();

    // Use Response Compression
    app.UseResponseCompression();

    // Use Static Files
    app.UseStaticFiles();

    // Use Swagger UI
    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }

            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.EnableValidator();
            options.DefaultModelsExpandDepth(-1);
            options.DisplayOperationId();
        });
    }

    // Use Health Checks
    app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.UseHealthChecks("/health/database", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("database")
    });

    app.UseHealthChecksUI(options =>
    {
        options.UIPath = "/health-ui";
        options.ApiPath = "/health-api";
    });

    // Use Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Use custom middleware
    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseMiddleware<PerformanceMiddleware>();
    app.UseMiddleware<ApiKeyMiddleware>();

    // Map Controllers
    app.MapControllers();

    // Global error handler
    app.UseExceptionHandler("/error");

    // Warm up cache
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
}

async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();

        var seeder = services.GetRequiredService<UJAS.Infrastructure.Seed.IDataSeeder>();
        await seeder.SeedAsync();

        Log.Information("Database initialized successfully");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while initializing the database");
        throw;
    }
}