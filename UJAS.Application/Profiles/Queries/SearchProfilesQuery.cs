using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Queries
{
    public class SearchProfilesQuery : IRequest<PaginatedList<ProfileSummaryDto>>
    {
        public Guid CompanyId { get; set; }
        public string SearchTerm { get; set; }
        public List<string> Skills { get; set; } = new();
        public List<string> JobTitles { get; set; } = new();
        public List<string> Locations { get; set; } = new();
        public List<string> EducationLevels { get; set; } = new();
        public int? MinYearsExperience { get; set; }
        public int? MaxYearsExperience { get; set; }
        public bool? IsCurrentlyEmployed { get; set; }
        public bool? WillingToRelocate { get; set; }
        public bool? WillingToTravel { get; set; }
        public decimal? MinSalaryExpectation { get; set; }
        public decimal? MaxSalaryExpectation { get; set; }
        public DateTime? ProfileUpdatedAfter { get; set; }
        public DateTime? ProfileUpdatedBefore { get; set; }
        public bool? IsProfileComplete { get; set; }
        public bool? HasResume { get; set; }
        public bool? HasReferences { get; set; }
        public string SortBy { get; set; } = "Relevance";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public SearchProfilesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
