using UJAS.Core.Interfaces;

namespace UJAS.Core.Events
{
    public class AssessmentCompletedEvent : IDomainEvent
    {
        public int ApplicationId { get; }
        public int AssessmentId { get; }
        public int Score { get; }
        public bool IsPassed { get; }
        public DateTime OccurredOn { get; }

        public AssessmentCompletedEvent(int applicationId, int assessmentId,
            int score, bool isPassed)
        {
            ApplicationId = applicationId;
            AssessmentId = assessmentId;
            Score = score;
            IsPassed = isPassed;
            OccurredOn = DateTime.UtcNow;
        }
    }
}