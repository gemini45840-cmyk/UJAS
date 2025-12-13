using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UJAS.Core.Entities.User;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Identity;
using UJAS.Infrastructure.Repositories.Base;
using UJAS.Infrastructure.Repositories.Implementations;
using UJAS.Infrastructure.Seed;
using UJAS.Infrastructure.Services.Email;
using UJAS.Infrastructure.Services.FileStorage;

namespace UJAS.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }));

            // Identity
            services.AddIdentity<User, Role>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
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
                    }
                };
            });

            // Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SystemAdminOnly", policy =>
                    policy.RequireRole("SystemAdministrator"));

                options.AddPolicy("CompanyAdminOnly", policy =>
                    policy.RequireRole("CompanyAdministrator"));

                options.AddPolicy("RegionalManagerOnly", policy =>
                    policy.RequireRole("RegionalManager"));

                options.AddPolicy("ManagerOnly", policy =>
                    policy.RequireRole("Manager"));

                options.AddPolicy("ApplicantOnly", policy =>
                    policy.RequireRole("Applicant"));
            });

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IDataSeeder, DataSeeder>();

            // Email Service
            var emailProvider = configuration["Email:Provider"];
            if (emailProvider == "SendGrid")
            {
                services.AddScoped<IEmailService, SendGridEmailService>();
            }
            else
            {
                // Fallback to SMTP or other provider
            }

            // File Storage Service
            var storageProvider = configuration["FileStorage:Provider"];
            if (storageProvider == "Azure")
            {
                services.AddScoped<IFileStorageService, AzureBlobStorageService>();
            }
            else
            {
                services.AddScoped<IFileStorageService, LocalFileStorageService>();
            }

            // Caching
            services.AddDistributedMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "UJAS_";
            });

            // Background Jobs
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer();

            // HttpContext Accessor
            services.AddHttpContextAccessor();

            // Health Checks
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>()
                .AddRedis(configuration.GetConnectionString("Redis"));

            // Configure Options
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
            services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            return services;
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedAsync();
        }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireDays { get; set; }
    }

    public class EmailSettings
    {
        public string Provider { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string SendGridApiKey { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }

    public class AzureStorageSettings
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }

    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string ClientUrl { get; set; }
        public bool UseHttps { get; set; }
        public string SupportEmail { get; set; }
    }
}