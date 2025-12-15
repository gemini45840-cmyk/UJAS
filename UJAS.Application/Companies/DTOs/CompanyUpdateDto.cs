using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.DTOs
{
    public class CompanyUpdateDto
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public int? EmployeeCount { get; set; }
        public string SizeCategory { get; set; }
        public string Status { get; set; }

        // Address
        public AddressUpdateDto HeadquartersAddress { get; set; }
        public AddressUpdateDto MailingAddress { get; set; }
        public bool? IsMailingSameAsHeadquarters { get; set; }

        // Branding
        public BrandingUpdateDto Branding { get; set; }

        // Settings
        public CompanySettingsUpdateDto Settings { get; set; }

        // Billing
        public BillingInfoUpdateDto BillingInfo { get; set; }

        // Subscription
        public SubscriptionUpdateDto Subscription { get; set; }
    }

    public class AddressUpdateDto
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

    public class BrandingUpdateDto
    {
        public string LogoUrl { get; set; }
        public string FaviconUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string AccentColor { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string ButtonColor { get; set; }
        public string ButtonTextColor { get; set; }
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string CustomCSS { get; set; }
        public string CustomJavaScript { get; set; }
        public string ApplicationHeaderText { get; set; }
        public string ApplicationFooterText { get; set; }
        public bool? ShowLogoOnApplication { get; set; }
        public bool? ShowCompanyNameOnApplication { get; set; }
    }

    public class CompanySettingsUpdateDto
    {
        public bool? AllowMultipleApplications { get; set; }
        public int? ApplicationCooldownDays { get; set; }
        public bool? RequireEmailVerification { get; set; }
        public bool? RequirePhoneVerification { get; set; }
        public bool? AutoSaveApplications { get; set; }
        public int? AutoSaveIntervalSeconds { get; set; }

        public bool? SendApplicationConfirmationEmail { get; set; }
        public bool? SendStatusUpdateEmails { get; set; }
        public bool? SendAssessmentInvitationEmails { get; set; }
        public bool? SendInterviewInvitationEmails { get; set; }
        public bool? SendOfferEmails { get; set; }
        public bool? SendRejectionEmails { get; set; }

        public bool? NotifyAdminsOnNewApplication { get; set; }
        public bool? NotifyManagersOnNewApplication { get; set; }
        public List<string> NotificationEmails { get; set; }

        public bool? CollectEEOData { get; set; }
        public bool? CollectDiversityData { get; set; }
        public bool? CollectDisabilityData { get; set; }
        public bool? CollectVeteranData { get; set; }
        public bool? AnonymizeApplicationsForReview { get; set; }
        public int? DataRetentionDays { get; set; }
        public bool? AutoDeleteExpiredData { get; set; }

        public bool? EnableWidget { get; set; }
        public string WidgetPosition { get; set; }
        public string WidgetLabel { get; set; }
        public bool? WidgetAutoOpen { get; set; }
        public int? WidgetDelaySeconds { get; set; }

        public string DefaultLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; }
        public bool? AutoDetectLanguage { get; set; }

        public bool? RequireStrongPasswords { get; set; }
        public int? PasswordMinLength { get; set; }
        public bool? RequireTwoFactorAuthentication { get; set; }
    }

    public class BillingInfoUpdateDto
    {
        public string BillingContactName { get; set; }
        public string BillingEmail { get; set; }
        public string BillingPhone { get; set; }
        public AddressUpdateDto BillingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string BillingCycle { get; set; }
        public string Currency { get; set; }
    }

    public class SubscriptionUpdateDto
    {
        public string Plan { get; set; }
        public bool? AutoRenew { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string Status { get; set; }
    }
}