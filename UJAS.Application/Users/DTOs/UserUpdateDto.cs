using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.DTOs
{
    public class UserUpdateDto
    {
        public Guid UserId { get; set; }

        // Basic information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string TimeZone { get; set; }
        public string PreferredLanguage { get; set; }

        // Status
        public string Status { get; set; } // Active, Inactive, Suspended

        // Preferences
        public UserPreferencesUpdateDto Preferences { get; set; }

        // Settings
        public UserSettingsUpdateDto Settings { get; set; }

        // Permissions
        public List<string> Permissions { get; set; }

        // Company/Location associations
        public List<Guid> AddLocationIds { get; set; } = new();
        public List<Guid> RemoveLocationIds { get; set; } = new();
        public List<UserRoleAssignmentDto> RoleAssignments { get; set; } = new();

        // Security
        public bool? RequirePasswordChange { get; set; }
        public bool? TwoFactorEnabled { get; set; }

        // Metadata
        public string UpdateReason { get; set; }
    }

    public class UserPreferencesUpdateDto
    {
        public string Theme { get; set; }
        public bool? EmailNotifications { get; set; }
        public bool? PushNotifications { get; set; }
        public bool? SmsNotifications { get; set; }
        public string NotificationFrequency { get; set; }
        public bool? AutoSaveForms { get; set; }
        public int? AutoSaveIntervalSeconds { get; set; }
        public string DashboardLayout { get; set; }
        public List<string> DashboardWidgets { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
    }

    public class UserSettingsUpdateDto
    {
        public bool? ShowWelcomeMessage { get; set; }
        public bool? ShowRecentActivities { get; set; }
        public bool? ShowStatistics { get; set; }
        public int? ItemsPerPage { get; set; }
        public bool? EnableKeyboardShortcuts { get; set; }
        public bool? AutoLogoutEnabled { get; set; }
        public int? AutoLogoutMinutes { get; set; }
        public string DefaultExportFormat { get; set; }
    }

    public class UserRoleAssignmentDto
    {
        public string RoleName { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public List<string> Permissions { get; set; } = new();
        public bool IsAddOperation { get; set; } = true; // True to add, False to remove
    }

    public class UserPasswordChangeDto
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public bool RequirePasswordChange { get; set; } = false;
    }

    public class UserPasswordResetDto
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string RecaptchaToken { get; set; }
    }

    public class UserEmailUpdateDto
    {
        public Guid UserId { get; set; }
        public string NewEmail { get; set; }
        public string CurrentPassword { get; set; }
        public bool RequireVerification { get; set; } = true;
    }
}
