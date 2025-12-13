using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Interfaces;

namespace UJAS.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            var email = _currentUserService.Email;

            _logger.LogInformation(
                "Handling {RequestName} for UserId: {UserId} ({Email})",
                requestName, userId, email);

            var startTime = DateTime.UtcNow;

            try
            {
                var response = await next();

                var elapsedTime = DateTime.UtcNow - startTime;
                _logger.LogInformation(
                    "Completed {RequestName} for UserId: {UserId} in {ElapsedMilliseconds}ms",
                    requestName, userId, elapsedTime.TotalMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error handling {RequestName} for UserId: {UserId} ({Email})",
                    requestName, userId, email);
                throw;
            }
        }
    }
}