using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.FirstName)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithMessage("First name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.LastName)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.LastName))
                .WithMessage("Last name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").When(x => !string.IsNullOrEmpty(x.LastName))
                .WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Valid phone number is required")
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone number cannot exceed 20 characters");

            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) || new[] { "Active", "Inactive", "Suspended", "Pending", "Locked" }.Contains(x))
                .WithMessage("Status must be Active, Inactive, Suspended, Pending, or Locked");

            RuleFor(x => x.TimeZone)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.TimeZone))
                .WithMessage("Time zone cannot exceed 50 characters");

            RuleFor(x => x.PreferredLanguage)
                .MaximumLength(10).When(x => !string.IsNullOrEmpty(x.PreferredLanguage))
                .WithMessage("Preferred language cannot exceed 10 characters");

            RuleForEach(x => x.Permissions)
                .MaximumLength(100).When(x => x.Permissions != null)
                .WithMessage("Permission cannot exceed 100 characters");

            When(x => x.Preferences != null, () =>
            {
                RuleFor(x => x.Preferences.Theme)
                    .Must(x => string.IsNullOrEmpty(x) || new[] { "Light", "Dark", "System" }.Contains(x))
                    .WithMessage("Theme must be Light, Dark, or System");

                RuleFor(x => x.Preferences.NotificationFrequency)
                    .Must(x => string.IsNullOrEmpty(x) || new[] { "Immediate", "Daily", "Weekly" }.Contains(x))
                    .WithMessage("Notification frequency must be Immediate, Daily, or Weekly");

                RuleFor(x => x.Preferences.AutoSaveIntervalSeconds)
                    .InclusiveBetween(5, 300).When(x => x.Preferences.AutoSaveIntervalSeconds.HasValue)
                    .WithMessage("Auto-save interval must be between 5 and 300 seconds");
            });

            When(x => x.Settings != null, () =>
            {
                RuleFor(x => x.Settings.ItemsPerPage)
                    .InclusiveBetween(5, 100).When(x => x.Settings.ItemsPerPage.HasValue)
                    .WithMessage("Items per page must be between 5 and 100");

                RuleFor(x => x.Settings.AutoLogoutMinutes)
                    .InclusiveBetween(1, 480).When(x => x.Settings.AutoLogoutMinutes.HasValue)
                    .WithMessage("Auto-logout minutes must be between 1 and 480");

                RuleFor(x => x.Settings.DefaultExportFormat)
                    .Must(x => string.IsNullOrEmpty(x) || new[] { "Excel", "CSV", "PDF", "JSON" }.Contains(x))
                    .WithMessage("Default export format must be Excel, CSV, PDF, or JSON");
            });
        }
    }

    public class UserPasswordChangeDtoValidator : AbstractValidator<UserPasswordChangeDto>
    {
        public UserPasswordChangeDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters")
                .MaximumLength(100).WithMessage("New password cannot exceed 100 characters")
                .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter")
                .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter")
                .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one number")
                .Matches(@"(?=.*[@$!%*?&])").WithMessage("New password must contain at least one special character")
                .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from current password");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }

    public class UserPasswordResetDtoValidator : AbstractValidator<UserPasswordResetDto>
    {
        public UserPasswordResetDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required");

            RuleFor(x => x.ResetToken)
                .NotEmpty().WithMessage("Reset token is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters")
                .MaximumLength(100).WithMessage("New password cannot exceed 100 characters")
                .Matches(@"(?=.*[a-z])").WithMessage("New password must contain at least one lowercase letter")
                .Matches(@"(?=.*[A-Z])").WithMessage("New password must contain at least one uppercase letter")
                .Matches(@"(?=.*\d)").WithMessage("New password must contain at least one number")
                .Matches(@"(?=.*[@$!%*?&])").WithMessage("New password must contain at least one special character");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }

    public class UserEmailUpdateDtoValidator : AbstractValidator<UserEmailUpdateDto>
    {
        public UserEmailUpdateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("New email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required for email change");
        }
    }
}
