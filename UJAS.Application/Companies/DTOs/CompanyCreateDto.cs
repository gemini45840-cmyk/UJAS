using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.DTOs
{
    public class CompanyCreateDto
    {
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
        public int EmployeeCount { get; set; } = 1;
        public string SizeCategory { get; set; } = "Small";

        // Address
        public AddressCreateDto HeadquartersAddress { get; set; }
        public AddressCreateDto MailingAddress { get; set; }
        public bool IsMailingSameAsHeadquarters { get; set; } = true;

        // Branding
        public BrandingCreateDto Branding { get; set; }

        // Settings
        public CompanySettingsCreateDto Settings { get; set; }

        // Initial Administrator
        public CompanyAdministratorCreateDto InitialAdministrator { get; set; }

        // Subscription
        public SubscriptionCreateDto Subscription { get; set; }
    }

    public class AddressCreateDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; } = "US";
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class BrandingCreateDto
    {
        public string PrimaryColor { get; set; } = "#0066CC";
        public string SecondaryColor { get; set; } = "#003366";
        public string AccentColor { get; set; } = "#FF6600";
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string TextColor { get; set; } = "#333333";
        public string ButtonColor { get; set; } = "#0066CC";
        public string ButtonTextColor { get; set; } = "#FFFFFF";
        public string FontFamily { get; set; } = "Arial, sans-serif";
        public string FontSize { get; set; } = "14px";
        public string ApplicationHeaderText { get; set; } = "Apply for a Position";
        public string ApplicationFooterText { get; set; } = "Thank you for your interest";
    }

    public class CompanySettingsCreateDto
    {
        public bool AllowMultipleApplications { get; set; } = true;
        public int ApplicationCooldownDays { get; set; } = 30;
        public bool RequireEmailVerification { get; set; } = true;
        public bool AutoSaveApplications { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;

        public bool SendApplicationConfirmationEmail { get; set; } = true;
        public bool SendStatusUpdateEmails { get; set; } = true;

        public bool CollectEEOData { get; set; } = true;
        public int DataRetentionDays { get; set; } = 365;
        public bool AutoDeleteExpiredData { get; set; } = true;

        public string DefaultLanguage { get; set; } = "en";
        public List<string> SupportedLanguages { get; set; } = new() { "en" };

        public bool RequireStrongPasswords { get; set; } = true;
        public int PasswordMinLength { get; set; } = 8;
    }

    public class CompanyAdministratorCreateDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsPrimary { get; set; } = true;
        public List<string> Permissions { get; set; } = new()
        {
            "ManageCompany",
            "ManageLocations",
            "ManageUsers",
            "ViewAnalytics",
            "ManageSettings"
        };
    }

    public class SubscriptionCreateDto
    {
        public string Plan { get; set; } = "Pro";
        public string BillingCycle { get; set; } = "Monthly";
        public bool AutoRenew { get; set; } = true;
        public string PaymentMethod { get; set; } = "CreditCard";
        public string Currency { get; set; } = "USD";
    }
}