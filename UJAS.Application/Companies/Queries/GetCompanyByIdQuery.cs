using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Base;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Application.Companies.Dtos;
using UJAS.Core.Specifications;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyByIdQuery : BaseQuery<CompanyDto>
    {
        public int CompanyId { get; set; }
        public bool IncludeLocations { get; set; } = false;
        public bool IncludeStatistics { get; set; } = false;
    }

    public class GetCompanyByIdQueryHandler : BaseHandler, IRequestHandler<GetCompanyByIdQuery, ApiResponse<CompanyDto>>
    {
        public GetCompanyByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            IDateTimeService dateTime,
            ILogger<GetCompanyByIdQueryHandler> logger)
            : base(unitOfWork, mapper, currentUser, dateTime, logger)
        {
        }

        public async Task<ApiResponse<CompanyDto>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Check access
                if (!await _currentUser.HasAccessToCompanyAsync(request.CompanyId))
                    return await HandleUnauthorizedAsync<CompanyDto>("view company");

                // Get company with includes
                var includes = new List<System.Linq.Expressions.Expression<Func<Core.Entities.Company.Company, object>>>();

                includes.Add(c => c.Settings);

                if (request.IncludeLocations)
                    includes.Add(c => c.Locations.Where(l => !l.IsDeleted));

                var company = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .GetEntityWithSpec(new CompanyByIdSpecification(request.CompanyId, includes));

                if (company == null)
                    return await HandleNotFoundAsync<CompanyDto>("Company", request.CompanyId);

                var companyDto = _mapper.Map<CompanyDto>(company);

                // Include statistics if requested
                if (request.IncludeStatistics)
                {
                    companyDto.Statistics = await GetCompanyStatisticsAsync(request.CompanyId);
                }

                return ApiResponse<CompanyDto>.SuccessResponse(companyDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company by ID {CompanyId}", request.CompanyId);
                return ApiResponse<CompanyDto>.FailureResponse("Error getting company");
            }
        }

        private async Task<CompanyStatisticsDto> GetCompanyStatisticsAsync(int companyId)
        {
            var applications = await _unitOfWork.Repository<Core.Entities.Application.Application>()
                .GetAsync(a => a.CompanyId == companyId && !a.IsDeleted);

            var locations = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                .GetAsync(l => l.CompanyId == companyId && !l.IsDeleted);

            var statistics = new CompanyStatisticsDto
            {
                TotalLocations = locations.Count,
                TotalApplications = applications.Count,
                ActiveApplications = applications.Count(a =>
                    a.Status != Core.Enums.ApplicationStatus.Rejected &&
                    a.Status != Core.Enums.ApplicationStatus.Withdrawn &&
                    a.Status != Core.Enums.ApplicationStatus.Archived),
                HiredCount = applications.Count(a => a.Status == Core.Enums.ApplicationStatus.Accepted),
                ApplicationsByLocation = applications
                    .GroupBy(a => a.LocationId)
                    .ToDictionary(g => g.Key.ToString(), g => g.Count()),
                ApplicationsByMonth = applications
                    .Where(a => a.SubmissionDate.HasValue)
                    .GroupBy(a => a.SubmissionDate.Value.ToString("yyyy-MM"))
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            // Calculate average hire time
            var acceptedApplications = applications
                .Where(a => a.Status == Core.Enums.ApplicationStatus.Accepted &&
                           a.SubmissionDate.HasValue &&
                           a.DecisionDate.HasValue)
                .ToList();

            if (acceptedApplications.Any())
            {
                statistics.AverageHireTime = acceptedApplications
                    .Average(a => (a.DecisionDate.Value - a.SubmissionDate.Value).TotalDays);
            }

            return statistics;
        }
    }

    public class CompanyByIdSpecification : BaseSpecification<Core.Entities.Company.Company>
    {
        public CompanyByIdSpecification(int companyId,
            List<System.Linq.Expressions.Expression<Func<Core.Entities.Company.Company, object>>> includes = null)
        {
            AddCriteria(c => c.Id == companyId && !c.IsDeleted);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    AddInclude(include);
                }
            }
        }
    }
}