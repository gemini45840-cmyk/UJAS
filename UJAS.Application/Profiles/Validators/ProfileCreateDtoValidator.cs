using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.Validators;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Validators
{
    public class ProfileCreateDtoValidator : AbstractValidator<ProfileCreateDto>
    {
        public ProfileCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[+]?[\d\s\-().]+$").WithMessage("Valid phone number is required")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");

            RuleFor(x => x.PersonalInformation)
                .NotNull().WithMessage("Personal information is required")
                .SetValidator(new PersonalInformationCreateDtoValidator());

            RuleFor(x => x.ProfileSettings)
                .NotNull().WithMessage("Profile settings are required")
                .SetValidator(new ProfileSettingsCreateDtoValidator());

            RuleFor(x => x.CommunicationPreferences)
                .NotNull().WithMessage("Communication preferences are required")
                .SetValidator(new CommunicationPreferencesCreateDtoValidator());
        }
    }

    public class PersonalInformationCreateDtoValidator : AbstractValidator<PersonalInformationCreateDto>
    {
        public PersonalInformationCreateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.MobilePhone)
                .NotEmpty().WithMessage("Mobile phone is required")
                .Matches(@"^[+]?[\d\s\-().]+$").WithMessage("Valid phone number is required");

            RuleFor(x => x.CurrentAddress)
                .NotNull().WithMessage("Current address is required")
                .SetValidator(new AddressCreateDtoValidator());

            RuleFor(x => x.PreferredContactMethod)
                .Must(x => new[] { "Email", "Phone", "SMS", "Any" }.Contains(x))
                .WithMessage("Preferred contact method must be Email, Phone, SMS, or Any");
        }
    }

    public class ProfileSettingsCreateDtoValidator : AbstractValidator<ProfileSettingsCreateDto>
    {
        public ProfileSettingsCreateDtoValidator()
        {
            RuleFor(x => x.ProfileVisibility)
                .Must(x => new[] { "Public", "Private", "SelectedCompanies" }.Contains(x))
                .WithMessage("Profile visibility must be Public, Private, or SelectedCompanies");

            RuleFor(x => x.PreferredLanguage)
                .NotEmpty().WithMessage("Preferred language is required")
                .Length(2, 10).WithMessage("Preferred language must be 2-10 characters");

            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("Time zone is required");
        }
    }

    public class CommunicationPreferencesCreateDtoValidator : AbstractValidator<CommunicationPreferencesCreateDto>
    {
        public CommunicationPreferencesCreateDtoValidator()
        {
            RuleFor(x => x.NotificationMethods)
                .NotEmpty().WithMessage("At least one notification method is required");

            RuleForEach(x => x.NotificationMethods)
                .Must(x => new[] { "Email", "SMS", "Push" }.Contains(x))
                .WithMessage("Notification method must be Email, SMS, or Push");
        }
    }
}