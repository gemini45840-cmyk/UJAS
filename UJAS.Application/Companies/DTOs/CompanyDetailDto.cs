using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Locations.Dtos;

namespace UJAS.Application.Companies.DTOs
{
    public class CompanyDetailDto : CompanyDto
    {
        // Company Administrators
        public List<CompanyAdministratorDto> Administrators { get; set; } = new();

        // Locations
        public List<LocationDto> Locations { get; set; } = new();

        // Custom Fields
        public List<CompanyCustomFieldDto> CustomFields { get; set; } = new();

        // Assessments
        public List<CompanyAssessmentDto> Assessments { get; set; } = new();

        // Integration Settings
        public IntegrationSettingsDto IntegrationSettings { get; set; }

        // Widget Embed Code
        public string WidgetEmbedCode { get; set; }

        // Audit Log
        public List<CompanyAuditLogDto> RecentAuditLogs { get; set; } = new();

        // Billing Information
        public BillingInfoDto BillingInfo { get; set; }

        // Subscription Information
        public SubscriptionDto Subscription { get; set; }
    }

    public class CompanyAdministratorDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } // Admin, SuperAdmin, BillingAdmin
        public bool IsPrimary { get; set; }
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public List<string> Permissions { get; set; } = new();
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CompanyCustomFieldDto
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string FieldType { get; set; } // Text, Number, Date, Dropdown, Checkbox, etc.
        public string DataType { get; set; } // string, int, decimal, DateTime, bool
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string Section { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }
        public string DefaultValue { get; set; }
        public string ValidationRules { get; set; } // JSON
        public List<string> Options { get; set; } = new(); // For dropdowns
        public bool IsVisibleToApplicant { get; set; } = true;
        public bool IsVisibleToManagers { get; set; } = true;
        public bool IsVisibleToAdmins { get; set; } = true;
        public bool IsSearchable { get; set; } = false;
        public bool IsFilterable { get; set; } = false;
        public bool IsExportable { get; set; } = true;
        public string PrivacyLevel { get; set; } // Public, Internal, Confidential
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CompanyAssessmentDto
    {
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public List<Guid> AssignedPositionIds { get; set; } = new();
        public List<Guid> AssignedLocationIds { get; set; } = new();
        public DateTime? AssignmentStartDate { get; set; }
        public DateTime? AssignmentEndDate { get; set; }
    }

    public class IntegrationSettingsDto
    {
        public bool EnableATSIntegration { get; set; } = false;
        public string ATSProvider { get; set; } // Greenhouse, Lever, Workday, etc.
        public string ATSApiKey { get; set; }
        public string ATSApiUrl { get; set; }

        public bool EnableHRISIntegration { get; set; } = false;
        public string HRISProvider { get; set; } // BambooHR, ADP, Gusto, etc.
        public string HRISApiKey { get; set; }
        public string HRISApiUrl { get; set; }

        public bool EnableBackgroundCheckIntegration { get; set; } = false;
        public string BackgroundCheckProvider { get; set; }
        public string BackgroundCheckApiKey { get; set; }

        public bool EnableEmailServiceIntegration { get; set; } = false;
        public string EmailServiceProvider { get; set; } // SendGrid, Mailgun, Amazon SES
        public string EmailServiceApiKey { get; set; }

        public bool EnableSMSServiceIntegration { get; set; } = false;
        public string SMSServiceProvider { get; set; } // Twilio, Nexmo, etc.
        public string SMSServiceApiKey { get; set; }

        public bool EnableCalendarIntegration { get; set; } = false;
        public string CalendarProvider { get; set; } // Google Calendar, Outlook
        public string CalendarApiKey { get; set; }

        public string WebhookSecret { get; set; }
        public List<WebhookEndpointDto> WebhookEndpoints { get; set; } = new();
    }

    public class WebhookEndpointDto
    {
        public string Url { get; set; }
        public List<string> Events { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public string Secret { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastTriggered { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }

    public class CompanyAuditLogDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string OldValues { get; set; } // JSON
        public string NewValues { get; set; } // JSON
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
    }

    public class BillingInfoDto
    {
        public string BillingContactName { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public AddressDto BillingAddress { get; set; }
        public string PaymentMethod { get; set; } // CreditCard, BankTransfer, Invoice
        public string CreditCardLastFour { get; set; }
        public DateTime? CreditCardExpiry { get; set; }
        public string BillingCycle { get; set; } // Monthly, Quarterly, Annually
        public DateTime? NextBillingDate { get; set; }
        public decimal MonthlyRate { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public decimal LastPaymentAmount { get; set; }
        public string Currency { get; set; } = "USD";
    }

    public class SubscriptionDto
    {
        public string Plan { get; set; } // Free, Basic, Pro, Enterprise
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public bool AutoRenew { get; set; } = true;
        public string Status { get; set; } // Active, Trial, Expired, Cancelled, Suspended
        public int MaxLocations { get; set; }
        public int MaxUsers { get; set; }
        public int MaxApplicationsPerMonth { get; set; }
        public int MaxAssessments { get; set; }
        public int MaxCustomFields { get; set; }
        public bool HasAdvancedAnalytics { get; set; }
        public bool HasCustomBranding { get; set; }
        public bool HasAPIAccess { get; set; }
        public bool HasPrioritySupport { get; set; }
        public List<string> Features { get; set; } = new();
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public DateTime? TrialEndDate { get; set; }
        public bool IsInTrial { get; set; }
    }
}
