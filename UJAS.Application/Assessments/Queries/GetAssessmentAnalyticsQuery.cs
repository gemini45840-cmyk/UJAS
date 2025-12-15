using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentAnalyticsQuery : IRequest<AssessmentAnalyticsDto>
    {
        public Guid AssessmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? PositionId { get; set; }
        public bool IncludeQuestionAnalytics { get; set; } = true;
        public bool IncludeDemographicAnalytics { get; set; } = false;

        public GetAssessmentAnalyticsQuery(Guid assessmentId)
        {
            AssessmentId = assessmentId;
        }
    }
}