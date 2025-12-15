using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Status { get; set; } // Active, Inactive, Complete, Incomplete
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int CompletionPercentage { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPublic { get; set; }

        // Statistics
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int CompletedAssessments { get; set; }
        public int SavedJobs { get; set; }

        // Preferences
        public List<string> JobPreferences { get; set; } = new();
        public List<string> LocationPreferences { get; set; } = new();

        // Privacy Settings
        public PrivacySettingsDto PrivacySettings { get; set; }
    }

    public class PrivacySettingsDto
    {
        public bool ShowProfileToCompanies { get; set; } = true;
        public bool AllowDataSharing { get; set; } = true;
        public bool ReceiveJobAlerts { get; set; } = true;
        public bool ReceiveMarketingEmails { get; set; } = false;
        public string DataRetentionPreference { get; set; } = "AutoDelete"; // AutoDelete, Keep, Manual
        public DateTime? DataRetentionDate { get; set; }
        public List<Guid> BlockedCompanies { get; set; } = new();
        public bool AnonymizeProfile { get; set; } = false;
    }
}