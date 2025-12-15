using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;
using UJAS.Application.Common;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentResponsesQuery : IRequest<PaginatedList<AssessmentResponseDto>>
    {
        public Guid AssessmentId { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid? ApplicationId { get; set; }
        public List<ResponseStatusDto> StatusFilter { get; set; } = new();
        public bool? Passed { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "CompletedDate";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool IncludeQuestionResponses { get; set; } = false;

        public GetAssessmentResponsesQuery(Guid assessmentId)
        {
            AssessmentId = assessmentId;
        }
    }
}
