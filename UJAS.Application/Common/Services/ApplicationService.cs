using AutoMapper;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Common.Services
{
    public interface IApplicationService
    {
        Task<ApiResponse<string>> GenerateApplicationNumberAsync(int companyId);
        Task<ApiResponse<List<ApplicationStatusHistoryDto>>> GetApplicationHistoryAsync(int applicationId);
        Task<ApiResponse<ApplicationMetricsDto>> GetApplicationMetricsAsync(int? companyId, int? locationId, DateTime? startDate, DateTime? endDate);
        Task<ApiResponse<List<ApplicationStatusDto>>> GetApplicationStatusesAsync();
        Task<ApiResponse<bool>> ValidateApplicationDataAsync(CreateApplicationDto applicationDto);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<ApplicationService> _logger;

        public ApplicationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            ILogger<ApplicationService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> GenerateApplicationNumberAsync(int companyId)
        {
            try
            {
                var company = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .GetByIdAsync(companyId);

                if (company == null)
                    return ApiResponse<string>.FailureResponse("Company not found");

                var today = DateTime.UtcNow;
                var year = today.ToString("yy");
                var month = today.ToString("MM");

                // Get sequence number for today
                var applicationsToday = await _unitOfWork.Repository<Core.Entities.Application.Application>()
                    .CountAsync(a => a.CompanyId == companyId &&
                                    a.CreatedAt.Date == today.Date);

                var sequence = applicationsToday + 1;
                var applicationNumber = $"APP-{companyId:D4}-{year}{month}-{sequence:D6}";

                return ApiResponse<string>.SuccessResponse(applicationNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating application number for company {CompanyId}", companyId);
                return ApiResponse<string>.FailureResponse("Error generating application number");
            }
        }

        public async Task<ApiResponse<List<ApplicationStatusHistoryDto>>> GetApplicationHistoryAsync(int applicationId)
        {
            try
            {
                if (!await _currentUser.HasAccessToApplicationAsync(applicationId))
                    return ApiResponse<List<ApplicationStatusHistoryDto>>.FailureResponse("Access denied", statusCode: 403);

                var history = await _unitOfWork.Repository<Core.Entities.Application.ApplicationStatusHistory>()
                    .GetAsync(h => h.ApplicationId == applicationId,
                        orderBy: q => q.OrderByDescending(h => h.CreatedAt));

                var historyDtos = _mapper.Map<List<ApplicationStatusHistoryDto>>(history);
                return ApiResponse<List<ApplicationStatusHistoryDto>>.SuccessResponse(historyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application history for application {ApplicationId}", applicationId);
                return ApiResponse<List<ApplicationStatusHistoryDto>>.FailureResponse("Error getting application history");
            }
        }

        public async Task<ApiResponse<ApplicationMetricsDto>> GetApplicationMetricsAsync(
            int? companyId, int? locationId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Validate access
                if (companyId.HasValue && !await _currentUser.HasAccessToCompanyAsync(companyId.Value))
                    return ApiResponse<ApplicationMetricsDto>.FailureResponse("Access denied", statusCode: 403);

                if (locationId.HasValue && !await _currentUser.HasAccessToLocationAsync(locationId.Value))
                    return ApiResponse<ApplicationMetricsDto>.FailureResponse("Access denied", statusCode: 403);

                var query = _unitOfWork.Repository<Core.Entities.Application.Application>()
                    .GetQueryable();

                // Apply filters
                if (companyId.HasValue)
                    query = query.Where(a => a.CompanyId == companyId.Value);

                if (locationId.HasValue)
                    query = query.Where(a => a.LocationId == locationId.Value);

                if (startDate.HasValue)
                    query = query.Where(a => a.CreatedAt >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(a => a.CreatedAt <= endDate.Value);

                var applications = await query.ToListAsync();

                var metrics = new ApplicationMetricsDto
                {
                    TotalApplications = applications.Count,
                    ApplicationsByStatus = applications
                        .GroupBy(a => a.Status)
                        .ToDictionary(g => g.Key.ToString(), g => g.Count()),
                    ApplicationsByDay = applications
                        .GroupBy(a => a.CreatedAt.Date)
                        .OrderBy(g => g.Key)
                        .ToDictionary(g => g.Key.ToString("yyyy-MM-dd"), g => g.Count()),
                    AverageCompletionTime = applications
                        .Where(a => a.CompletionTimeMinutes.HasValue)
                        .Average(a => a.CompletionTimeMinutes) ?? 0,
                    SubmissionSources = applications
                        .GroupBy(a => a.ReferralSource)
                        .Where(g => !string.IsNullOrEmpty(g.Key))
                        .ToDictionary(g => g.Key, g => g.Count())
                };

                return ApiResponse<ApplicationMetricsDto>.SuccessResponse(metrics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting application metrics");
                return ApiResponse<ApplicationMetricsDto>.FailureResponse("Error getting metrics");
            }
        }

        public async Task<ApiResponse<List<ApplicationStatusDto>>> GetApplicationStatusesAsync()
        {
            var statuses = Enum.GetValues(typeof(Core.Enums.ApplicationStatus))
                .Cast<Core.Enums.ApplicationStatus>()
                .Select(s => new ApplicationStatusDto
                {
                    Id = (int)s,
                    Name = s.ToString(),
                    DisplayName = GetStatusDisplayName(s),
                    Description = GetStatusDescription(s),
                    IsActive = IsActiveStatus(s),
                    IsFinal = IsFinalStatus(s)
                })
                .ToList();

            return ApiResponse<List<ApplicationStatusDto>>.SuccessResponse(statuses);
        }

        public async Task<ApiResponse<bool>> ValidateApplicationDataAsync(CreateApplicationDto applicationDto)
        {
            try
            {
                var errors = new List<string>();

                // Validate required fields
                if (string.IsNullOrEmpty(applicationDto.PositionAppliedFor))
                    errors.Add("Position applied for is required");

                if (applicationDto.CompanyId <= 0)
                    errors.Add("Company is required");

                if (applicationDto.LocationId <= 0)
                    errors.Add("Location is required");

                if (applicationDto.ApplicantProfileId <= 0)
                    errors.Add("Applicant profile is required");

                // Validate company exists
                var company = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .GetByIdAsync(applicationDto.CompanyId);

                if (company == null)
                    errors.Add("Company not found");

                // Validate location exists and belongs to company
                var location = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                    .GetSingleAsync(l => l.Id == applicationDto.LocationId && l.CompanyId == applicationDto.CompanyId);

                if (location == null)
                    errors.Add("Location not found or doesn't belong to the company");

                // Validate applicant profile exists
                var applicantProfile = await _unitOfWork.Repository<Core.Entities.Profile.ApplicantProfile>()
                    .GetByIdAsync(applicationDto.ApplicantProfileId);

                if (applicantProfile == null)
                    errors.Add("Applicant profile not found");

                // Validate no duplicate application for same position
                var existingApplication = await _unitOfWork.Repository<Core.Entities.Application.Application>()
                    .GetSingleAsync(a => a.ApplicantProfileId == applicationDto.ApplicantProfileId &&
                                        a.CompanyId == applicationDto.CompanyId &&
                                        a.LocationId == applicationDto.LocationId &&
                                        a.PositionAppliedFor == applicationDto.PositionAppliedFor &&
                                        !a.IsDeleted);

                if (existingApplication != null)
                    errors.Add("You have already applied for this position");

                if (errors.Any())
                    return ApiResponse<bool>.FailureResponse("Validation failed", errors);

                return ApiResponse<bool>.SuccessResponse(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating application data");
                return ApiResponse<bool>.FailureResponse("Error validating application");
            }
        }

        private string GetStatusDisplayName(Core.Enums.ApplicationStatus status)
        {
            return status switch
            {
                Core.Enums.ApplicationStatus.Draft => "Draft",
                Core.Enums.ApplicationStatus.Submitted => "Submitted",
                Core.Enums.ApplicationStatus.UnderReview => "Under Review",
                Core.Enums.ApplicationStatus.Shortlisted => "Shortlisted",
                Core.Enums.ApplicationStatus.InterviewScheduled => "Interview Scheduled",
                Core.Enums.ApplicationStatus.AssessmentRequired => "Assessment Required",
                Core.Enums.ApplicationStatus.BackgroundCheck => "Background Check",
                Core.Enums.ApplicationStatus.Accepted => "Accepted",
                Core.Enums.ApplicationStatus.Rejected => "Rejected",
                Core.Enums.ApplicationStatus.Withdrawn => "Withdrawn",
                Core.Enums.ApplicationStatus.Archived => "Archived",
                _ => status.ToString()
            };
        }

        private string GetStatusDescription(Core.Enums.ApplicationStatus status)
        {
            return status switch
            {
                Core.Enums.ApplicationStatus.Draft => "Application is being drafted",
                Core.Enums.ApplicationStatus.Submitted => "Application has been submitted",
                Core.Enums.ApplicationStatus.UnderReview => "Application is under review",
                Core.Enums.ApplicationStatus.Shortlisted => "Applicant has been shortlisted",
                Core.Enums.ApplicationStatus.InterviewScheduled => "Interview has been scheduled",
                Core.Enums.ApplicationStatus.AssessmentRequired => "Assessment required",
                Core.Enums.ApplicationStatus.BackgroundCheck => "Background check in progress",
                Core.Enums.ApplicationStatus.Accepted => "Application accepted",
                Core.Enums.ApplicationStatus.Rejected => "Application rejected",
                Core.Enums.ApplicationStatus.Withdrawn => "Application withdrawn by applicant",
                Core.Enums.ApplicationStatus.Archived => "Application archived",
                _ => string.Empty
            };
        }

        private bool IsActiveStatus(Core.Enums.ApplicationStatus status)
        {
            var activeStatuses = new[]
            {
                Core.Enums.ApplicationStatus.Draft,
                Core.Enums.ApplicationStatus.Submitted,
                Core.Enums.ApplicationStatus.UnderReview,
                Core.Enums.ApplicationStatus.Shortlisted,
                Core.Enums.ApplicationStatus.InterviewScheduled,
                Core.Enums.ApplicationStatus.AssessmentRequired,
                Core.Enums.ApplicationStatus.BackgroundCheck
            };

            return activeStatuses.Contains(status);
        }

        private bool IsFinalStatus(Core.Enums.ApplicationStatus status)
        {
            var finalStatuses = new[]
            {
                Core.Enums.ApplicationStatus.Accepted,
                Core.Enums.ApplicationStatus.Rejected,
                Core.Enums.ApplicationStatus.Withdrawn,
                Core.Enums.ApplicationStatus.Archived
            };

            return finalStatuses.Contains(status);
        }
    }

    public class ApplicationMetricsDto
    {
        public int TotalApplications { get; set; }
        public Dictionary<string, int> ApplicationsByStatus { get; set; } = new();
        public Dictionary<string, int> ApplicationsByDay { get; set; } = new();
        public double AverageCompletionTime { get; set; }
        public Dictionary<string, int> SubmissionSources { get; set; } = new();
        public int NewApplicationsToday { get; set; }
        public int ApplicationsThisWeek { get; set; }
        public int ApplicationsThisMonth { get; set; }
    }

    public class ApplicationStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsFinal { get; set; }
    }
}