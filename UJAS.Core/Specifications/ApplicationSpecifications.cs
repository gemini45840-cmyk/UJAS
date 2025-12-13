using UJAS.Core.Entities.Application;
using UJAS.Core.Enums;
using UJAS.Core.Helpers;

namespace UJAS.Core.Specifications
{
    public class ApplicationsByCompanySpecification : BaseSpecification<tApplication>
    {
        public ApplicationsByCompanySpecification(int companyId,
            ApplicationStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var criteria = PredicateBuilder.True<tApplication>();
            criteria = criteria.And(a => a.CompanyId == companyId && !a.IsDeleted);

            if (status.HasValue)
            {
                criteria = criteria.And(a => a.Status == status.Value);
            }

            if (fromDate.HasValue)
            {
                criteria = criteria.And(a => a.CreatedAt >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                criteria = criteria.And(a => a.CreatedAt <= toDate.Value);
            }

            AddCriteria(criteria);
            AddInclude(a => a.ApplicantProfile);
            AddInclude(a => a.Location);
            ApplyOrderByDescending(a => a.CreatedAt);
        }
    }

    public class ApplicationsByApplicantSpecification : BaseSpecification<tApplication>
    {
        public ApplicationsByApplicantSpecification(int applicantProfileId)
        {
            AddCriteria(a => a.ApplicantProfileId == applicantProfileId && !a.IsDeleted);
            AddInclude(a => a.Company);
            AddInclude(a => a.Location);
            ApplyOrderByDescending(a => a.CreatedAt);
        }
    }

    public class ApplicationsByManagerSpecification : BaseSpecification<tApplication>
    {
        public ApplicationsByManagerSpecification(int locationId,
            ApplicationStatus? status = null)
        {
            var criteria = PredicateBuilder.True<tApplication>();
            criteria = criteria.And(a => a.LocationId == locationId && !a.IsDeleted);

            if (status.HasValue)
            {
                criteria = criteria.And(a => a.Status == status.Value);
            }

            AddCriteria(criteria);
            AddInclude(a => a.ApplicantProfile);
            ApplyOrderByDescending(a => a.CreatedAt);
        }
    }
}