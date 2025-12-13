using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UJAS.Application.Common.Models;

namespace UJAS.API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly IWebHostEnvironment _environment;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Log the exception
            _logger.LogError(exception, "Unhandled exception occurred");

            // Handle different exception types
            var (statusCode, message) = exception switch
            {
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access"),
                ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                InvalidOperationException => (StatusCodes.Status400BadRequest, exception.Message),
                NotImplementedException => (StatusCodes.Status501NotImplemented, "Feature not implemented"),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            // Create error response
            var response = ApiResponse.FailureResponse(
                message,
                _environment.IsDevelopment() ? new List<string> { exception.ToString() } : null,
                statusCode
            );

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}