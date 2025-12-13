using UJAS.Core.Entities.Assessment;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Application
{
    public class tApplication : BaseAuditableEntity
    {
        public string ApplicationNumber { get; set; }
        public int ApplicantProfileId { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public string PositionAppliedFor { get; set; }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Draft;
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string RejectionReason { get; set; }
        public string WithdrawalReason { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignTrackingCode { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public int? CompletionTimeMinutes { get; set; }
        public int ApplicationVersion { get; set; } = 1;
        public DateTime? RetentionEndDate { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
        public virtual tCompany Company { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<ApplicationAnswer> ApplicationAnswers { get; set; } = new List<ApplicationAnswer>();
        public virtual ICollection<ApplicationStatusHistory> StatusHistory { get; set; } = new List<ApplicationStatusHistory>();
        public virtual ICollection<ApplicationComment> Comments { get; set; } = new List<ApplicationComment>();
        public virtual ICollection<ApplicationAssessment> Assessments { get; set; } = new List<ApplicationAssessment>();
        public virtual ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
    }
}