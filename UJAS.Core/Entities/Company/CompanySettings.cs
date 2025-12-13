namespace UJAS.Core.Entities.Company
{
    public class CompanySettings : BaseEntity
    {
        public int CompanyId { get; set; }
        public bool RequireAssessment { get; set; }
        public bool EnableBackgroundCheck { get; set; }
        public bool EnableDrugTest { get; set; }
        public int ApplicationRetentionDays { get; set; } = 365;
        public bool AutoReplyToApplicants { get; set; }
        public string AutoReplyMessage { get; set; }
        public bool AllowApplicationWithdraw { get; set; }
        public bool ShowSalaryFields { get; set; }
        public bool CollectEEOData { get; set; }
        public string DefaultLanguage { get; set; } = "en-US";
        public string DateFormat { get; set; } = "MM/dd/yyyy";

        // Navigation properties
        public virtual Company Company { get; set; }
    }
}