using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class ReviewAssessmentResponseCommand : IRequest<bool>
    {
        public Guid ResponseId { get; set; }
        public string ReviewNotes { get; set; }
        public string Decision { get; set; } // Accept, Reject, Flag, NeedMoreInfo
        public Guid ReviewedBy { get; set; }
        public bool IsProctoringReview { get; set; } = false;
        public List<ProctoringReviewDto> ProctoringReviews { get; set; } = new();

        public ReviewAssessmentResponseCommand(Guid responseId, Guid reviewedBy)
        {
            ResponseId = responseId;
            ReviewedBy = reviewedBy;
        }
    }

    public class ProctoringReviewDto
    {
        public Guid EventId { get; set; }
        public string ReviewNotes { get; set; }
        public string Severity { get; set; } // Low, Medium, High, Critical
        public bool IsFalsePositive { get; set; }
        public bool RequiresAction { get; set; }
        public string ActionTaken { get; set; }
    }
}
