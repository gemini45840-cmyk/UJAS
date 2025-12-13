using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace UJAS.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.RoutePrefix = "api-docs";
                options.DocumentTitle = "UJAS API Documentation";
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.EnableDeepLinking();
                options.EnableFilter();
                options.EnableValidator();
                options.DefaultModelsExpandDepth(2);
                options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.ShowExtensions();
                options.ShowCommonExtensions();
                options.InjectStylesheet("/swagger-ui/custom.css");
            });

            return app;
        }
    }
}