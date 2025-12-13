using System.Diagnostics;

namespace UJAS.API.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers["X-Response-Time"] = $"{stopwatch.ElapsedMilliseconds}ms";
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}