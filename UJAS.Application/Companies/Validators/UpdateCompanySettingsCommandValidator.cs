using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.Commands;

{
    public class UpdateCompanySettingsCommandValidator : AbstractValidator<UpdateCompanySettingsCommand>
{
    public UpdateCompanySettingsCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Company ID is required");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("Updated by user ID is required");

        When(x => x.Settings != null, () =>
        {
            RuleFor(x => x.Settings.ApplicationCooldownDays)
                .InclusiveBetween(0, 365).When(x => x.Settings.ApplicationCooldownDays.HasValue)
                .WithMessage("Application cooldown days must be between 0 and 365");

            RuleFor(x => x.Settings.AutoSaveIntervalSeconds)
                .InclusiveBetween(5, 300).When(x => x.Settings.AutoSaveIntervalSeconds.HasValue)
                .WithMessage("Auto-save interval must be between 5 and 300 seconds");

            RuleFor(x => x.Settings.DataRetentionDays)
                .InclusiveBetween(30, 3650).When(x => x.Settings.DataRetentionDays.HasValue)
                .WithMessage("Data retention days must be between 30 and 3650");

            RuleFor(x => x.Settings.PasswordMinLength)
                .InclusiveBetween(6, 50).When(x => x.Settings.PasswordMinLength.HasValue)
                .WithMessage("Password minimum length must be between 6 and 50");

            RuleFor(x => x.Settings.MaxLoginAttempts)
                .InclusiveBetween(1, 10).When(x => x.Settings.MaxLoginAttempts.HasValue)
                .WithMessage("Maximum login attempts must be between 1 and 10");

            RuleFor(x => x.Settings.LockoutMinutes)
                .InclusiveBetween(1, 1440).When(x => x.Settings.LockoutMinutes.HasValue)
                .WithMessage("Lockout minutes must be between 1 and 1440");

            RuleFor(x => x.Settings.WidgetDelaySeconds)
                .InclusiveBetween(0, 60).When(x => x.Settings.WidgetDelaySeconds.HasValue)
                .WithMessage("Widget delay seconds must be between 0 and 60");

            RuleForEach(x => x.Settings.NotificationEmails)
                .EmailAddress().When(x => x.Settings.NotificationEmails != null)
                .WithMessage("Valid email address is required");
        });
    }
}
}
