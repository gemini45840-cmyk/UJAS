using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; } // EIN/VAT/etc.
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime EstablishedDate { get; set; }
        public int EmployeeCount { get; set; }
        public string SizeCategory { get; set; } // Small, Medium, Large, Enterprise
        public string Status { get; set; } // Active, Inactive, Suspended, Pending

        // Address
        public AddressDto HeadquartersAddress { get; set; }
        public AddressDto MailingAddress { get; set; }
        public bool IsMailingSameAsHeadquarters { get; set; }

        // Branding
        public BrandingDto Branding { get; set; }

        // Settings
        public CompanySettingsDto Settings { get; set; }

        // Statistics
        public CompanyStatisticsDto Statistics { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    public class AddressDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string TimeZone { get; set; }
    }

    public class BrandingDto
    {
        public string LogoUrl { get; set; }
        public string FaviconUrl { get; set; }
        public string PrimaryColor { get; set; } // Hex code
        public string SecondaryColor { get; set; } // Hex code
        public string AccentColor { get; set; } // Hex code
        public string BackgroundColor { get; set; } // Hex code
        public string TextColor { get; set; } // Hex code
        public string ButtonColor { get; set; } // Hex code
        public string ButtonTextColor { get; set; } // Hex code
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string CustomCSS { get; set; }
        public string CustomJavaScript { get; set; }
        public string ApplicationHeaderText { get; set; }
        public string ApplicationFooterText { get; set; }
        public bool ShowLogoOnApplication { get; set; } = true;
        public bool ShowCompanyNameOnApplication { get; set; } = true;
        public string EmailTemplateBackgroundColor { get; set; }
        public string EmailTemplateHeaderColor { get; set; }
        public string EmailTemplateFooterColor { get; set; }
    }

    public class CompanySettingsDto
    {
        // Application Settings
        public bool AllowMultipleApplications { get; set; } = true;
        public int ApplicationCooldownDays { get; set; } = 30;
        public bool RequireEmailVerification { get; set; } = true;
        public bool RequirePhoneVerification { get; set; } = false;
        public bool AutoSaveApplications { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;

        // Communication Settings
        public bool SendApplicationConfirmationEmail { get; set; } = true;
        public bool SendStatusUpdateEmails { get; set; } = true;
        public bool SendAssessmentInvitationEmails { get; set; } = true;
        public bool SendInterviewInvitationEmails { get; set; } = true;
        public bool SendOfferEmails { get; set; } = true;
        public bool SendRejectionEmails { get; set; } = true;
        public bool SendWithdrawalConfirmationEmails { get; set; } = true;

        // Notification Settings
        public bool NotifyAdminsOnNewApplication { get; set; } = true;
        public bool NotifyManagersOnNewApplication { get; set; } = true;
        public bool NotifyOnApplicationStatusChange { get; set; } = true;
        public bool NotifyOnAssessmentCompletion { get; set; } = true;
        public List<string> NotificationEmails { get; set; } = new();

        // Privacy & Compliance
        public bool CollectEEOData { get; set; } = true;
        public bool CollectDiversityData { get; set; } = true;
        public bool CollectDisabilityData { get; set; } = true;
        public bool CollectVeteranData { get; set; } = true;
        public bool AnonymizeApplicationsForReview { get; set; } = false;
        public int DataRetentionDays { get; set; } = 365;
        public bool AutoDeleteExpiredData { get; set; } = true;
        public bool AllowDataExport { get; set; } = true;
        public bool RequireGDPRConsent { get; set; } = true;
        public bool RequireCCPAConsent { get; set; } = true;
        public string PrivacyPolicyUrl { get; set; }
        public string TermsOfServiceUrl { get; set; }

        // Assessment Settings
        public bool RequireAssessments { get; set; } = false;
        public int AssessmentTimeLimitExtensionMinutes { get; set; } = 0;
        public bool AllowAssessmentRetakes { get; set; } = false;
        public int MaxAssessmentAttempts { get; set; } = 1;
        public int AssessmentCooldownHours { get; set; } = 24;

        // Integration Settings
        public bool EnableAPI { get; set; } = false;
        public string APIKey { get; set; }
        public DateTime? APIKeyExpires { get; set; }
        public bool EnableWebhook { get; set; } = false;
        public string WebhookUrl { get; set; }
        public List<string> WebhookEvents { get; set; } = new();

        // Widget Settings
        public bool EnableWidget { get; set; } = true;
        public string WidgetPosition { get; set; } = "bottom-right"; // bottom-right, bottom-left, top-right, top-left
        public string WidgetLabel { get; set; } = "Apply Now";
        public bool WidgetAutoOpen { get; set; } = false;
        public int WidgetDelaySeconds { get; set; } = 5;

        // Localization
        public string DefaultLanguage { get; set; } = "en";
        public List<string> SupportedLanguages { get; set; } = new() { "en" };
        public bool AutoDetectLanguage { get; set; } = true;

        // Security
        public bool RequireStrongPasswords { get; set; } = true;
        public int PasswordMinLength { get; set; } = 8;
        public bool RequirePasswordHistory { get; set; } = true;
        public int PasswordHistoryCount { get; set; } = 5;
        public int MaxLoginAttempts { get; set; } = 5;
        public int LockoutMinutes { get; set; } = 15;
        public bool RequireTwoFactorAuthentication { get; set; } = false;
        public bool RequireSSO { get; set; } = false;
        public string SSOProvider { get; set; }
        public string SSOConfiguration { get; set; } // JSON configuration
    }

    public class CompanyStatisticsDto
    {
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int TotalLocations { get; set; }
        public int ActiveLocations { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalAdministrators { get; set; }
        public int TotalManagers { get; set; }
        public int TotalAssessments { get; set; }
        public int TotalCustomFields { get; set; }
        public DateTime? LastApplicationDate { get; set; }
        public decimal ApplicationCompletionRate { get; set; }
        public decimal AverageApplicationTimeMinutes { get; set; }
    }
}