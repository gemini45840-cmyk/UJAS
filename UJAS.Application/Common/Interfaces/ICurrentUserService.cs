using Microsoft.AspNetCore.Http;
using UJAS.Core.Enums;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        List<string> Roles { get; }
        int? CompanyId { get; }
        int? LocationId { get; }
        bool IsSystemAdmin { get; }
        bool IsCompanyAdmin { get; }
        bool IsRegionalManager { get; }
        bool IsManager { get; }
        bool IsApplicant { get; }
        UserRole? PrimaryRole { get; }

        Task<bool> HasPermissionAsync(string permission);
        Task<bool> HasAccessToApplicationAsync(int applicationId);
        Task<bool> HasAccessToLocationAsync(int locationId);
        Task<bool> HasAccessToCompanyAsync(int companyId);
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public int? UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value
                    ?? _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                return int.TryParse(userIdClaim, out var userId) ? userId : null;
            }
        }

        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        public bool IsAuthenticated => UserId.HasValue;

        public List<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims
            .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? new List<string>();

        public int? CompanyId
        {
            get
            {
                var companyIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("company_id")?.Value;
                return int.TryParse(companyIdClaim, out var companyId) ? companyId : null;
            }
        }

        public int? LocationId
        {
            get
            {
                var locationIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("location_id")?.Value;
                return int.TryParse(locationIdClaim, out var locationId) ? locationId : null;
            }
        }

        public bool IsSystemAdmin => Roles.Contains("SystemAdministrator");
        public bool IsCompanyAdmin => Roles.Contains("CompanyAdministrator");
        public bool IsRegionalManager => Roles.Contains("RegionalManager");
        public bool IsManager => Roles.Contains("Manager");
        public bool IsApplicant => Roles.Contains("Applicant");

        public UserRole? PrimaryRole
        {
            get
            {
                if (IsSystemAdmin) return UserRole.SystemAdministrator;
                if (IsCompanyAdmin) return UserRole.CompanyAdministrator;
                if (IsRegionalManager) return UserRole.RegionalManager;
                if (IsManager) return UserRole.Manager;
                if (IsApplicant) return UserRole.Applicant;
                return null;
            }
        }

        public async Task<bool> HasPermissionAsync(string permission)
        {
            if (!UserId.HasValue) return false;

            var user = await _unitOfWork.Repository<Core.Entities.User.tUser>()
                .GetSingleAsync(u => u.Id == UserId.Value,
                    includes: new List<System.Linq.Expressions.Expression<Func<Core.Entities.User.tUser, object>>>
                    {
                        u => u.UserRoles,
                        u => u.UserRoles.Select(ur => ur.Role)
                    });

            if (user == null) return false;

            // Implement permission checking logic
            // This is a simplified version - you might want to cache permissions
            return true;
        }

        public async Task<bool> HasAccessToApplicationAsync(int applicationId)
        {
            if (!UserId.HasValue) return false;

            var application = await _unitOfWork.Repository<Core.Entities.Application.tApplication>()
                .GetSingleAsync(a => a.Id == applicationId,
                    includes: new List<System.Linq.Expressions.Expression<Func<Core.Entities.Application.tApplication, object>>>
                    {
                        a => a.Location
                    });

            if (application == null) return false;

            // System admin has access to all applications
            if (IsSystemAdmin) return true;

            // Company admin has access to all applications in their company
            if (IsCompanyAdmin && CompanyId.HasValue && application.CompanyId == CompanyId.Value)
                return true;

            // Regional manager has access to applications in their assigned locations
            if (IsRegionalManager && await HasAccessToLocationAsync(application.LocationId))
                return true;

            // Manager has access to applications in their location
            if (IsManager && LocationId.HasValue && application.LocationId == LocationId.Value)
                return true;

            // Applicant can only access their own applications
            if (IsApplicant && await IsApplicationOwnerAsync(applicationId))
                return true;

            return false;
        }

        public async Task<bool> HasAccessToLocationAsync(int locationId)
        {
            if (!UserId.HasValue) return false;

            var location = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                .GetByIdAsync(locationId);

            if (location == null) return false;

            // System admin has access to all locations
            if (IsSystemAdmin) return true;

            // Company admin has access to all locations in their company
            if (IsCompanyAdmin && CompanyId.HasValue && location.CompanyId == CompanyId.Value)
                return true;

            // Regional manager has access to assigned locations
            if (IsRegionalManager)
            {
                var companyUser = await _unitOfWork.Repository<Core.Entities.User.CompanyUser>()
                    .GetSingleAsync(cu => cu.UserId == UserId.Value && cu.IsRegionalManager,
                        includes: new List<System.Linq.Expressions.Expression<Func<Core.Entities.User.CompanyUser, object>>>
                        {
                            cu => cu.RegionalManagerLocations
                        });

                if (companyUser?.RegionalManagerLocations.Any(rl => rl.LocationId == locationId) == true)
                    return true;
            }

            // Manager has access to their assigned location
            if (IsManager && LocationId.HasValue && locationId == LocationId.Value)
                return true;

            return false;
        }

        public async Task<bool> HasAccessToCompanyAsync(int companyId)
        {
            if (!UserId.HasValue) return false;

            // System admin has access to all companies
            if (IsSystemAdmin) return true;

            // Company admin has access to their company
            if (IsCompanyAdmin && CompanyId.HasValue && companyId == CompanyId.Value)
                return true;

            // Check if user has any role in the company
            var companyUser = await _unitOfWork.Repository<Core.Entities.User.CompanyUser>()
                .GetSingleAsync(cu => cu.UserId == UserId.Value && cu.CompanyId == companyId);

            return companyUser != null;
        }

        private async Task<bool> IsApplicationOwnerAsync(int applicationId)
        {
            if (!UserId.HasValue) return false;

            var application = await _unitOfWork.Repository<Core.Entities.Application.tApplication>()
                .GetSingleAsync(a => a.Id == applicationId && a.ApplicantProfile.UserId == UserId.Value);

            return application != null;
        }
    }
}