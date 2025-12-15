using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;
using UJAS.Application.Common;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentsByCompanyQuery : IRequest<PaginatedList<AssessmentDto>>
    {
        public Guid CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public AssessmentTypeDto? Type { get; set; }
        public AssessmentCategoryDto? Category { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // Filter by assignment
        public Guid? PositionId { get; set; }
        public Guid? LocationId { get; set; }
        public string JobTitle { get; set; }

        public GetAssessmentsByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}