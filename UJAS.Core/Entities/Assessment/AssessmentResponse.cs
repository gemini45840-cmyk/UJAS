namespace UJAS.Core.Entities.Assessment
{
    public class AssessmentResponse : BaseEntity
    {
        public int ApplicationAssessmentId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public bool? IsCorrect { get; set; }
        public int? PointsAwarded { get; set; }
        public DateTime AnsweredAt { get; set; }
        public TimeSpan? TimeTaken { get; set; }

        // Navigation properties
        public virtual ApplicationAssessment ApplicationAssessment { get; set; }
        public virtual AssessmentQuestion Question { get; set; }
    }
}