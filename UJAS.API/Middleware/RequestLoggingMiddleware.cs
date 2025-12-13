namespace UJAS.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            var request = context.Request;

            // Log request
            _logger.LogInformation("Request: {Method} {Path} from {RemoteIpAddress}",
                request.Method, request.Path, context.Connection.RemoteIpAddress);

            // Copy original response body stream
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                var elapsed = DateTime.UtcNow - startTime;

                // Log response
                _logger.LogInformation("Response: {StatusCode} in {ElapsedMilliseconds}ms",
                    context.Response.StatusCode, elapsed.TotalMilliseconds);

                // Log slow requests
                if (elapsed.TotalMilliseconds > 1000)
                {
                    _logger.LogWarning("Slow request: {Method} {Path} took {ElapsedMilliseconds}ms",
                        request.Method, request.Path, elapsed.TotalMilliseconds);
                }

                // Copy response body back
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing request: {Method} {Path}",
                    request.Method, request.Path);
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}