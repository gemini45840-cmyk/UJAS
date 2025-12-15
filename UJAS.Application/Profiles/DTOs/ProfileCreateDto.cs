using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Profiles.DTOs
{
    public class ProfileCreateDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        // Basic Personal Information
        public PersonalInformationCreateDto PersonalInformation { get; set; }

        // Initial Settings
        public ProfileSettingsCreateDto ProfileSettings { get; set; }

        // Communication Preferences
        public CommunicationPreferencesCreateDto CommunicationPreferences { get; set; }

        // Optional: Import from external source
        public bool ImportFromLinkedIn { get; set; } = false;
        public string LinkedInAccessToken { get; set; }
        public bool ImportFromResume { get; set; } = false;
        public string ResumeBase64 { get; set; }
        public string ResumeFileName { get; set; }
    }

    public class PersonalInformationCreateDto
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public AddressCreateDto CurrentAddress { get; set; }
        public string PreferredContactMethod { get; set; } = "Email";
    }

    public class ProfileSettingsCreateDto
    {
        public string ProfileVisibility { get; set; } = "Private";
        public bool AutoFillApplications { get; set; } = true;
        public bool SaveApplicationDrafts { get; set; } = true;
        public bool ReceiveJobAlerts { get; set; } = true;
        public string PreferredLanguage { get; set; } = "en";
        public string TimeZone { get; set; }
    }

    public class CommunicationPreferencesCreateDto
    {
        public bool ReceiveApplicationUpdates { get; set; } = true;
        public bool ReceiveAssessmentInvitations { get; set; } = true;
        public bool ReceiveInterviewInvitations { get; set; } = true;
        public bool ReceiveSMSNotifications { get; set; } = false;
        public bool AllowMarketingEmails { get; set; } = false;
        public List<string> NotificationMethods { get; set; } = new() { "Email" };
    }
}