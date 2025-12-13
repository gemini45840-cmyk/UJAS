using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Base;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Application.Companies.Dtos;
using UJAS.Core.Helpers;
using UJAS.Core.Specifications;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompaniesQuery : PaginatedQuery<CompanyDto>
    {
        public bool? IsActive { get; set; }
        public string Industry { get; set; }
        public DateTime? SubscriptionExpiresBefore { get; set; }
        public bool IncludeStatistics { get; set; } = false;
    }

    public class GetCompaniesQueryHandler : BaseHandler, IRequestHandler<GetCompaniesQuery, ApiResponse<PaginatedResponse<CompanyDto>>>
    {
        public GetCompaniesQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            IDateTimeService dateTime,
            ILogger<GetCompaniesQueryHandler> logger)
            : base(unitOfWork, mapper, currentUser, dateTime, logger)
        {
        }

        public async Task<ApiResponse<PaginatedResponse<CompanyDto>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Only system administrators can view all companies
                // Company admins can only view their own company
                if (!_currentUser.IsSystemAdmin && !_currentUser.IsCompanyAdmin)
                    return await HandleUnauthorizedAsync<PaginatedResponse<CompanyDto>>("view companies");

                var spec = new CompaniesSpecification(
                    request.SearchTerm,
                    request.IsActive,
                    request.Industry,
                    request.SubscriptionExpiresBefore,
                    request.PageNumber,
                    request.PageSize,
                    request.SortBy,
                    request.SortDescending);

                var companies = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .ListAsync(spec);

                var totalCount = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .CountAsync(spec);

                var companyDtos = _mapper.Map<List<CompanyDto>>(companies);

                // Include statistics if requested
                if (request.IncludeStatistics)
                {
                    foreach (var companyDto in companyDtos)
                    {
                        if (_currentUser.IsSystemAdmin ||
                            (_currentUser.IsCompanyAdmin && _currentUser.CompanyId == companyDto.Id))
                        {
                            companyDto.Statistics = await GetCompanyStatisticsAsync(companyDto.Id);
                        }
                    }
                }

                var response = new PaginatedResponse<CompanyDto>(
                    companyDtos, totalCount, request.PageNumber, request.PageSize);

                return ApiResponse<PaginatedResponse<CompanyDto>>.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting companies");
                return ApiResponse<PaginatedResponse<CompanyDto>>.FailureResponse("Error getting companies");
            }
        }

        private async Task<CompanyStatisticsDto> GetCompanyStatisticsAsync(int companyId)
        {
            // Similar to previous implementation
            var applications = await _unitOfWork.Repository<Core.Entities.Application.Application>()
                .CountAsync(a => a.CompanyId == companyId && !a.IsDeleted);

            var locations = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                .CountAsync(l => l.CompanyId == companyId && !l.IsDeleted);

            return new CompanyStatisticsDto
            {
                TotalLocations = locations,
                TotalApplications = applications
            };
        }
    }

    public class CompaniesSpecification : BaseSpecification<Core.Entities.Company.Company>
    {
        public CompaniesSpecification(
            string searchTerm,
            bool? isActive,
            string industry,
            DateTime? subscriptionExpiresBefore,
            int pageNumber,
            int pageSize,
            string sortBy,
            bool sortDescending)
        {
            var criteria = PredicateBuilder.True<Core.Entities.Company.Company>();

            criteria = criteria.And(c => !c.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                criteria = criteria.And(c =>
                    c.Name.Contains(searchTerm) ||
                    c.LegalName.Contains(searchTerm) ||
                    c.Industry.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                criteria = criteria.And(c => c.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(industry))
            {
                criteria = criteria.And(c => c.Industry == industry);
            }

            if (subscriptionExpiresBefore.HasValue)
            {
                criteria = criteria.And(c => c.SubscriptionEndDate <= subscriptionExpiresBefore.Value);
            }

            AddCriteria(criteria);
            AddInclude(c => c.Settings);

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortDescending)
                    ApplyOrderByDescending(GetSortExpression(sortBy));
                else
                    ApplyOrderBy(GetSortExpression(sortBy));
            }
            else
            {
                ApplyOrderBy(c => c.Name);
            }

            ApplyPaging(pageNumber, pageSize);
        }

        private System.Linq.Expressions.Expression<Func<Core.Entities.Company.Company, object>> GetSortExpression(string sortBy)
        {
            return sortBy.ToLower() switch
            {
                "name" => c => c.Name,
                "industry" => c => c.Industry,
                "createdat" => c => c.CreatedAt,
                "subscriptionenddate" => c => c.SubscriptionEndDate,
                _ => c => c.Name
            };
        }
    }
}