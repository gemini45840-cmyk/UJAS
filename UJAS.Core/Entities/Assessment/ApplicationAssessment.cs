using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Assessment
{
    public class ApplicationAssessment : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int AssessmentId { get; set; }
        public AssessmentStatus Status { get; set; } = AssessmentStatus.NotStarted;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? Score { get; set; }
        public int AttemptCount { get; set; } = 0;
        public bool IsPassed { get; set; }
        public string ResultsJson { get; set; } // JSON containing detailed results
        public DateTime? ExpiresAt { get; set; }

        // Navigation properties
        public virtual Application Application { get; set; }
        public virtual Assessment Assessment { get; set; }
        public virtual ICollection<AssessmentResponse> Responses { get; set; } = new List<AssessmentResponse>();
    }
}