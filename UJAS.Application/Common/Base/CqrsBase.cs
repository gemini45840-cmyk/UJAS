using AutoMapper;
using MediatR;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Common.Base
{
    // Base classes for commands
    public abstract class BaseCommand<TResponse> : IRequest<ApiResponse<TResponse>> { }
    public abstract class BaseCommand : BaseCommand<Unit> { }

    // Base classes for queries
    public abstract class BaseQuery<TResponse> : IRequest<ApiResponse<TResponse>> { }
    public abstract class PaginatedQuery<TResponse> : BaseQuery<PaginatedResponse<TResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
        public string SearchTerm { get; set; }
    }

    // Base handler with common dependencies
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUser;
        protected readonly IDateTimeService _dateTime;
        protected readonly ILogger _logger;

        protected BaseHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            IDateTimeService dateTime,
            ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _dateTime = dateTime;
            _logger = logger;
        }

        protected virtual async Task<bool> ValidateUserAccessAsync(int companyId, int? locationId = null)
        {
            if (!_currentUser.IsAuthenticated)
                return false;

            if (_currentUser.IsSystemAdmin)
                return true;

            if (_currentUser.IsCompanyAdmin && _currentUser.CompanyId == companyId)
                return true;

            if (locationId.HasValue && await _currentUser.HasAccessToLocationAsync(locationId.Value))
                return true;

            return false;
        }

        protected virtual async Task<ApiResponse<T>> HandleNotFoundAsync<T>(string entityName, int id)
        {
            _logger.LogWarning("{Entity} with ID {Id} not found", entityName, id);
            return ApiResponse<T>.FailureResponse($"{entityName} not found", statusCode: 404);
        }

        protected virtual async Task<ApiResponse<T>> HandleUnauthorizedAsync<T>(string action)
        {
            _logger.LogWarning("User {UserId} unauthorized to {Action}", _currentUser.UserId, action);
            return ApiResponse<T>.FailureResponse("You are not authorized to perform this action", statusCode: 403);
        }
    }
}