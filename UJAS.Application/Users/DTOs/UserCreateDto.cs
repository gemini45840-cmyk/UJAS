using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.DTOs
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserType { get; set; } // SystemAdmin, CompanyAdmin, RegionalManager, Manager, Applicant

        // Company/Location associations
        public Guid? CompanyId { get; set; }
        public List<Guid> LocationIds { get; set; } = new();
        public string Role { get; set; }

        // Permissions
        public List<string> Permissions { get; set; } = new();

        // Settings
        public UserPreferencesDto Preferences { get; set; }
        public UserSettingsDto Settings { get; set; }

        // Options
        public bool SendWelcomeEmail { get; set; } = true;
        public bool RequireEmailVerification { get; set; } = true;
        public bool RequirePasswordChange { get; set; } = false;
        public bool IsActive { get; set; } = true;

        // SSO Information
        public bool IsSsoUser { get; set; } = false;
        public string SsoProvider { get; set; }
        public string SsoExternalId { get; set; }
    }

    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Optional profile information
        public string DesiredJobTitle { get; set; }
        public string CurrentLocation { get; set; }

        // Terms acceptance
        public bool AcceptTerms { get; set; }
        public bool AcceptPrivacyPolicy { get; set; }
        public bool ConsentToMarketing { get; set; }

        // Security
        public string RecaptchaToken { get; set; }

        // Referral
        public string ReferralCode { get; set; }
        public Guid? ReferredByUserId { get; set; }
    }

    public class ApplicantCreateDto : UserCreateDto
    {
        public Guid? ProfileId { get; set; }
        public bool CreateProfile { get; set; } = true;
        public bool ImportFromResume { get; set; } = false;
        public string ResumeBase64 { get; set; }
    }
}
