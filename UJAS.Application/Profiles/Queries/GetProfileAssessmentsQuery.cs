using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileAssessmentsQuery : IRequest<PaginatedList<AssessmentHistoryDto>>
    {
        public Guid ProfileId { get; set; }
        public List<string> Statuses { get; set; } = new();
        public Guid? CompanyId { get; set; }
        public bool? Passed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SortBy { get; set; } = "CompletedDate";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public GetProfileAssessmentsQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}