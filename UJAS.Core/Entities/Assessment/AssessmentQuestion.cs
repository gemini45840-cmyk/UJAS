namespace UJAS.Core.Entities.Assessment
{
    public class AssessmentQuestion : BaseEntity
    {
        public int AssessmentId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } // MultipleChoice, Essay, Rating, etc.
        public string Options { get; set; } // JSON for multiple choice
        public string CorrectAnswer { get; set; }
        public int Points { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; } = true;
        public string HelpText { get; set; }

        // Navigation properties
        public virtual tAssessment Assessment { get; set; }
    }
}
