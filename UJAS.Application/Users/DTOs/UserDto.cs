using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Users.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string UserType { get; set; } // SystemAdmin, CompanyAdmin, RegionalManager, Manager, Applicant
        public string Status { get; set; } // Active, Inactive, Suspended, Pending, Locked
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string TimeZone { get; set; }
        public string PreferredLanguage { get; set; }

        // Role-specific associations
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Guid> LocationIds { get; set; } = new();
        public List<string> LocationNames { get; set; } = new();
        public Guid? ProfileId { get; set; } // For applicants

        // Statistics
        public int LoginCount { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public bool IsLockedOut { get; set; }

        // Permissions summary
        public List<string> Permissions { get; set; } = new();
        public List<string> Roles { get; set; } = new();
    }

    public class UserDetailDto : UserDto
    {
        // Extended user information
        public UserPreferencesDto Preferences { get; set; }
        public UserSecurityDto Security { get; set; }
        public UserActivityDto Activity { get; set; }
        public UserSettingsDto Settings { get; set; }

        // Associations
        public List<UserCompanyDto> CompanyAssociations { get; set; } = new();
        public List<UserLocationDto> LocationAssociations { get; set; } = new();
        public List<UserRoleDto> UserRoles { get; set; } = new();

        // Audit
        public List<UserAuditLogDto> RecentActivity { get; set; } = new();
        public List<LoginHistoryDto> RecentLogins { get; set; } = new();
    }

    public class UserPreferencesDto
    {
        public string Theme { get; set; } = "Light"; // Light, Dark, System
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = true;
        public bool SmsNotifications { get; set; } = false;
        public string NotificationFrequency { get; set; } = "Immediate"; // Immediate, Daily, Weekly
        public bool AutoSaveForms { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;
        public string DashboardLayout { get; set; }
        public List<string> DashboardWidgets { get; set; } = new();
        public string DefaultView { get; set; }
        public bool ShowTutorials { get; set; } = true;
        public bool ShowAnalytics { get; set; } = true;
        public bool CompactMode { get; set; } = false;
        public string DateFormat { get; set; } = "MM/dd/yyyy";
        public string TimeFormat { get; set; } = "hh:mm tt";
        public string NumberFormat { get; set; } = "en-US";
    }

    public class UserSecurityDto
    {
        public bool RequirePasswordChange { get; set; }
        public DateTime? PasswordLastChanged { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public List<string> PasswordHistory { get; set; } = new(); // Hashed passwords
        public List<SecurityQuestionDto> SecurityQuestions { get; set; } = new();
        public List<RecoveryMethodDto> RecoveryMethods { get; set; } = new();
        public List<TrustedDeviceDto> TrustedDevices { get; set; } = new();
        public List<ApiKeyDto> ApiKeys { get; set; } = new();
        public List<SsoConnectionDto> SsoConnections { get; set; } = new();
        public bool IsSsoUser { get; set; }
        public string SsoProvider { get; set; }
        public string SsoExternalId { get; set; }
    }

    public class UserActivityDto
    {
        public int TotalApplicationsViewed { get; set; }
        public int TotalAssessmentsGraded { get; set; }
        public int TotalCommentsPosted { get; set; }
        public int TotalNotificationsSent { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string LastActivityType { get; set; }
        public string LastActivityDetails { get; set; }
        public int SessionsThisMonth { get; set; }
        public int SessionsTotal { get; set; }
        public TimeSpan AverageSessionDuration { get; set; }
        public List<MostActiveTimeDto> MostActiveTimes { get; set; } = new();
        public List<FrequentActionDto> FrequentActions { get; set; } = new();
    }

    public class UserSettingsDto
    {
        public bool ShowWelcomeMessage { get; set; } = true;
        public bool ShowRecentActivities { get; set; } = true;
        public bool ShowStatistics { get; set; } = true;
        public int ItemsPerPage { get; set; } = 20;
        public bool EnableKeyboardShortcuts { get; set; } = true;
        public bool EnableVoiceCommands { get; set; } = false;
        public bool EnableScreenReader { get; set; } = false;
        public string HighContrastMode { get; set; } = "None";
        public string FontSize { get; set; } = "Medium";
        public string ColorBlindMode { get; set; } = "None";
        public bool AutoLogoutEnabled { get; set; } = true;
        public int AutoLogoutMinutes { get; set; } = 30;
        public bool RememberLastTab { get; set; } = true;
        public string DefaultExportFormat { get; set; } = "Excel";
        public List<string> HiddenColumns { get; set; } = new();
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }

    public class UserCompanyDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public bool IsPrimary { get; set; }
        public List<string> Permissions { get; set; } = new();
        public List<UserLocationDto> AssignedLocations { get; set; } = new();
    }

    public class UserLocationDto
    {
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; } // Manager, RegionalManager
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public bool IsPrimary { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    public class UserRoleDto
    {
        public string RoleName { get; set; }
        public string RoleType { get; set; } // System, Company, Location
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    public class UserAuditLogDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
    }

    public class SecurityQuestionDto
    {
        public string Question { get; set; }
        public string AnswerHash { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }

    public class RecoveryMethodDto
    {
        public string Method { get; set; } // Email, Phone, Authenticator
        public string Value { get; set; }
        public bool IsVerified { get; set; }
        public DateTime VerifiedDate { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class TrustedDeviceDto
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public bool IsTrusted { get; set; }
        public DateTime? TrustedDate { get; set; }
    }

    public class ApiKeyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string KeyPrefix { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> Permissions { get; set; } = new();
        public string Description { get; set; }
    }

    public class SsoConnectionDto
    {
        public string Provider { get; set; }
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public DateTime ConnectedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new();
    }

    public class MostActiveTimeDto
    {
        public string DayOfWeek { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public int ActivityCount { get; set; }
    }

    public class FrequentActionDto
    {
        public string Action { get; set; }
        public int Count { get; set; }
        public DateTime LastPerformed { get; set; }
    }
}
