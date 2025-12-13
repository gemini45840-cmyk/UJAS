using Microsoft.AspNetCore.Authorization;
using UJAS.Application.Common.Interfaces;

namespace UJAS.API.Services
{
    public interface ICompanyAccessService
    {
        Task<bool> HasAccessAsync(int companyId, int? locationId = null);
    }

    public class CompanyAccessService : ICompanyAccessService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthorizationService _authorizationService;

        public CompanyAccessService(
            ICurrentUserService currentUserService,
            IAuthorizationService authorizationService)
        {
            _currentUserService = currentUserService;
            _authorizationService = authorizationService;
        }

        public async Task<bool> HasAccessAsync(int companyId, int? locationId = null)
        {
            if (!_currentUserService.IsAuthenticated)
                return false;

            // System admins have access to everything
            if (_currentUserService.IsSystemAdmin)
                return true;

            // Check if user has access to the company
            if (!await _currentUserService.HasAccessToCompanyAsync(companyId))
                return false;

            // If location is specified, check location access
            if (locationId.HasValue && !await _currentUserService.HasAccessToLocationAsync(locationId.Value))
                return false;

            return true;
        }
    }

    public class CompanyAccessRequirement : IAuthorizationRequirement { }

    public class CompanyAccessHandler : AuthorizationHandler<CompanyAccessRequirement>
    {
        private readonly ICompanyAccessService _companyAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyAccessHandler(
            ICompanyAccessService companyAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _companyAccessService = companyAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CompanyAccessRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return;

            // Extract companyId from route
            var companyId = GetCompanyIdFromRoute(httpContext);
            if (!companyId.HasValue)
            {
                context.Fail();
                return;
            }

            // Extract locationId from route
            var locationId = GetLocationIdFromRoute(httpContext);

            if (await _companyAccessService.HasAccessAsync(companyId.Value, locationId))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        private int? GetCompanyIdFromRoute(HttpContext httpContext)
        {
            if (httpContext.Request.RouteValues.TryGetValue("companyId", out var companyIdValue) &&
                int.TryParse(companyIdValue?.ToString(), out var companyId))
            {
                return companyId;
            }

            // Check for companyId in query string
            if (httpContext.Request.Query.TryGetValue("companyId", out var queryCompanyId) &&
                int.TryParse(queryCompanyId, out companyId))
            {
                return companyId;
            }

            // Check for id parameter (might be company id)
            if (httpContext.Request.RouteValues.TryGetValue("id", out var idValue) &&
                int.TryParse(idValue?.ToString(), out var id))
            {
                // Need to determine if this is a company id based on controller
                var controller = httpContext.Request.RouteValues["controller"]?.ToString();
                if (controller?.Equals("Companies", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return id;
                }
            }

            return null;
        }

        private int? GetLocationIdFromRoute(HttpContext httpContext)
        {
            if (httpContext.Request.RouteValues.TryGetValue("locationId", out var locationIdValue) &&
                int.TryParse(locationIdValue?.ToString(), out var locationId))
            {
                return locationId;
            }

            if (httpContext.Request.Query.TryGetValue("locationId", out var queryLocationId) &&
                int.TryParse(queryLocationId, out locationId))
            {
                return locationId;
            }

            return null;
        }
    }
}