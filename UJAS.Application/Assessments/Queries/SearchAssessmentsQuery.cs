using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;
using UJAS.Application.Common;

namespace UJAS.Application.Assessments.Queries
{
    public class SearchAssessmentsQuery : IRequest<PaginatedList<AssessmentDto>>
    {
        public Guid CompanyId { get; set; }
        public string SearchTerm { get; set; }
        public List<AssessmentTypeDto> Types { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public int? MinQuestions { get; set; }
        public int? MaxQuestions { get; set; }
        public int? MinTimeMinutes { get; set; }
        public int? MaxTimeMinutes { get; set; }
        public bool? HasSections { get; set; }
        public bool? HasProctoring { get; set; }
        public bool? IsRequired { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string CreatedBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; } = "Relevance";

        public SearchAssessmentsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}