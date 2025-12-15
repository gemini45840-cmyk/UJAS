using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class GradeAssessmentCommand : IRequest<bool>
    {
        public Guid ResponseId { get; set; }
        public Dictionary<Guid, int> QuestionScores { get; set; } = new();
        public string OverallFeedback { get; set; }
        public Guid GradedBy { get; set; }
        public bool AutoGrade { get; set; } = false;
        public bool OverrideAutoGrade { get; set; } = false;

        public GradeAssessmentCommand(Guid responseId, Guid gradedBy)
        {
            ResponseId = responseId;
            GradedBy = gradedBy;
        }
    }
}
