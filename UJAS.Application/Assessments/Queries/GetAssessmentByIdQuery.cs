using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentByIdQuery : IRequest<AssessmentDetailDto>
    {
        public Guid AssessmentId { get; set; }
        public bool IncludeQuestions { get; set; } = true;
        public bool IncludeSections { get; set; } = true;
        public bool IncludeScoring { get; set; } = true;
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }

        public GetAssessmentByIdQuery(Guid assessmentId)
        {
            AssessmentId = assessmentId;
        }
    }
}
