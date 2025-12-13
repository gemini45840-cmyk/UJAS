using Microsoft.AspNetCore.Authorization;

namespace UJAS.API.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IApiKeyService apiKeyService)
        {
            var endpoint = context.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;

            // Skip API key validation for anonymous endpoints
            if (allowAnonymous)
            {
                await _next(context);
                return;
            }

            // Check for API key in header
            if (context.Request.Headers.TryGetValue("X-API-Key", out var apiKeyHeader))
            {
                var apiKey = apiKeyHeader.ToString();

                if (await apiKeyService.ValidateApiKeyAsync(apiKey))
                {
                    var apiKeyInfo = await apiKeyService.GetApiKeyInfoAsync(apiKey);
                    if (apiKeyInfo != null)
                    {
                        // Add API key info to context for authorization
                        context.Items["ApiKeyInfo"] = apiKeyInfo;
                        await _next(context);
                        return;
                    }
                }
            }

            // If no valid API key, continue with normal authentication
            await _next(context);
        }
    }
}