using UJAS.Core.Enums;

namespace UJAS.Core.Specifications
{
    public class ApplicationsByCompanySpecification : BaseSpecification<Application>
    {
        public ApplicationsByCompanySpecification(int companyId,
            ApplicationStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var criteria = PredicateBuilder.True<Application>();
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

    public class ApplicationsByApplicantSpecification : BaseSpecification<Application>
    {
        public ApplicationsByApplicantSpecification(int applicantProfileId)
        {
            AddCriteria(a => a.ApplicantProfileId == applicantProfileId && !a.IsDeleted);
            AddInclude(a => a.Company);
            AddInclude(a => a.Location);
            ApplyOrderByDescending(a => a.CreatedAt);
        }
    }

    public class ApplicationsByManagerSpecification : BaseSpecification<Application>
    {
        public ApplicationsByManagerSpecification(int locationId,
            ApplicationStatus? status = null)
        {
            var criteria = PredicateBuilder.True<Application>();
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