using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Models;

namespace UJAS.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Any())
                {
                    _logger.LogWarning("Validation failed for {RequestType}: {Failures}",
                        typeof(TRequest).Name, string.Join(", ", failures.Select(f => f.ErrorMessage)));

                    // Check if response is ApiResponse
                    var responseType = typeof(TResponse);
                    if (responseType.IsGenericType &&
                        responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                    {
                        var dataType = responseType.GetGenericArguments()[0];
                        var failureMethod = typeof(ApiResponse<>)
                            .MakeGenericType(dataType)
                            .GetMethod("FailureResponse", new[] { typeof(string), typeof(List<string>), typeof(int) });

                        var errors = failures.Select(f => f.ErrorMessage).ToList();
                        return (TResponse)failureMethod.Invoke(null,
                            new object[] { "Validation failed", errors, 400 });
                    }
                }
            }

            return await next();
        }
    }
}