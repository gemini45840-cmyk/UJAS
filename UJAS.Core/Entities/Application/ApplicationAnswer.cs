namespace UJAS.Core.Entities.Application
{
    public class ApplicationAnswer : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int CompanyFieldId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerJson { get; set; } // For complex answers
        public string FilePath { get; set; } // For file uploads
        public DateTime? AnswerDate { get; set; }

        // Navigation properties
        public virtual Application Application { get; set; }
        public virtual CompanyField CompanyField { get; set; }
    }
}