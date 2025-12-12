using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UJAS.Core.Enums;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Entities.System;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Applications
{
    public class Application : BaseEntity
    {
        // Basic Info
        public string ApplicationNumber { get; set; } // Auto-generated format: APP-YYYYMMDD-0001
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;

        // Relationships
        public int ApplicantId { get; set; }
        public virtual User Applicant { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string PositionAppliedFor { get; set; }

        // Application Data
        public string ApplicantSnapshot { get; set; } // JSON snapshot of profile at time of application
        public string CustomFieldsData { get; set; } // JSON of company-specific custom fields
        public string AssessmentResponses { get; set; } // JSON of assessment answers

        // Tracking
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignCode { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int CompletionTimeSeconds { get; set; }
        public int Version { get; set; } = 1;

        // Agreements
        public bool TruthfulnessStatement { get; set; }
        public bool BackgroundCheckAuthorization { get; set; }
        public bool DrugTestingConsent { get; set; }
        public bool EmploymentVerificationAuthorization { get; set; }
        public bool ReferenceCheckAuthorization { get; set; }
        public bool DataPrivacyConsent { get; set; }
        public string ElectronicSignature { get; set; }
        public DateTime? SignedAt { get; set; }

        // Status History
        public string StatusHistory { get; set; } // JSON array of status changes

        // Rejection/Withdrawal
        public RejectionReason? RejectionReason { get; set; }
        public string RejectionDetails { get; set; }
        public string WithdrawalReason { get; set; }

        // Navigation Properties
        public virtual ICollection<ApplicationStatus> StatusChanges { get; set; } = new List<ApplicationStatus>();
        public virtual ICollection<ApplicationComment> Comments { get; set; } = new List<ApplicationComment>();
        public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}