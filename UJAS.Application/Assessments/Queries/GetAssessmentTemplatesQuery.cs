using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;
using UJAS.Application.Common;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentTemplatesQuery : IRequest<PaginatedList<AssessmentTemplateDto>>
    {
        public AssessmentTypeDto? Type { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public string SearchTerm { get; set; }
        public bool? IsPublic { get; set; } = true;
        public string SortBy { get; set; } = "UsageCount";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // For company-specific templates
        public Guid? CompanyId { get; set; }
    }
}
