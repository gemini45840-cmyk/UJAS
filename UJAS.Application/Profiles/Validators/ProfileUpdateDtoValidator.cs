using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Validators
{
    public class ProfileUpdateDtoValidator : AbstractValidator<ProfileUpdateDto>
    {
        public ProfileUpdateDtoValidator()
        {
            RuleFor(x => x.ProfileId)
                .NotEmpty().WithMessage("Profile ID is required");

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

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required")
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone number cannot exceed 20 characters");

            When(x => x.PersonalInformation != null, () =>
            {
                RuleFor(x => x.PersonalInformation)
                    .SetValidator(new PersonalInformationUpdateDtoValidator());
            });

            When(x => x.EducationHistory != null, () =>
            {
                RuleForEach(x => x.EducationHistory)
                    .SetValidator(new EducationHistoryUpdateDtoValidator());
            });

            When(x => x.WorkExperience != null, () =>
            {
                RuleForEach(x => x.WorkExperience)
                    .SetValidator(new WorkExperienceUpdateDtoValidator());
            });

            When(x => x.References != null, () =>
            {
                RuleForEach(x => x.References)
                    .SetValidator(new ReferenceUpdateDtoValidator());
            });

            When(x => x.EmergencyContacts != null, () =>
            {
                RuleForEach(x => x.EmergencyContacts)
                    .SetValidator(new EmergencyContactUpdateDtoValidator());
            });
        }
    }

    public class PersonalInformationUpdateDtoValidator : AbstractValidator<PersonalInformationUpdateDto>
    {
        public PersonalInformationUpdateDtoValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.MobilePhone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.MobilePhone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today.AddYears(-16)).When(x => x.DateOfBirth.HasValue)
                .WithMessage("Applicant must be at least 16 years old")
                .GreaterThan(DateTime.Today.AddYears(-100)).When(x => x.DateOfBirth.HasValue)
                .WithMessage("Date of birth is invalid");

            RuleFor(x => x.LinkedInUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.LinkedInUrl))
                .WithMessage("Valid LinkedIn URL is required");

            RuleFor(x => x.GitHubUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.GitHubUrl))
                .WithMessage("Valid GitHub URL is required");

            RuleFor(x => x.PortfolioUrl)
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.PortfolioUrl))
                .WithMessage("Valid portfolio URL is required");

            When(x => x.CurrentAddress != null, () =>
            {
                RuleFor(x => x.CurrentAddress)
                    .SetValidator(new AddressUpdateDtoValidator());
            });

            When(x => x.MailingAddress != null, () =>
            {
                RuleFor(x => x.MailingAddress)
                    .SetValidator(new AddressUpdateDtoValidator());
            });
        }

        private bool BeValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    public class EducationHistoryUpdateDtoValidator : AbstractValidator<EducationHistoryUpdateDto>
    {
        public EducationHistoryUpdateDtoValidator()
        {
            RuleFor(x => x.InstitutionName)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Institution name is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.InstitutionName))
                .WithMessage("Institution name cannot exceed 100 characters");

            RuleFor(x => x.DegreeCertificate)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.DegreeCertificate))
                .WithMessage("Degree/Certificate cannot exceed 100 characters");

            RuleFor(x => x.FieldOfStudy)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.FieldOfStudy))
                .WithMessage("Field of study cannot exceed 100 characters");

            RuleFor(x => x.GPA)
                .InclusiveBetween(0, 4).When(x => x.GPA.HasValue)
                .WithMessage("GPA must be between 0 and 4");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.GraduationDate).When(x => x.StartDate.HasValue && x.GraduationDate.HasValue)
                .WithMessage("Start date must be before graduation date");
        }
    }

    public class WorkExperienceUpdateDtoValidator : AbstractValidator<WorkExperienceUpdateDto>
    {
        public WorkExperienceUpdateDtoValidator()
        {
            RuleFor(x => x.EmployerName)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Employer name is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.EmployerName))
                .WithMessage("Employer name cannot exceed 100 characters");

            RuleFor(x => x.JobTitle)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Job title is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.JobTitle))
                .WithMessage("Job title cannot exceed 100 characters");

            RuleFor(x => x.StartDate)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Start date is required");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue && x.StartDate.HasValue)
                .WithMessage("End date must be after start date")
                .LessThan(DateTime.Today).When(x => x.EndDate.HasValue)
                .WithMessage("End date cannot be in the future");
        }
    }

    public class ReferenceUpdateDtoValidator : AbstractValidator<ReferenceUpdateDto>
    {
        public ReferenceUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Reference name is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Reference name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required");

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");
        }
    }

    public class EmergencyContactUpdateDtoValidator : AbstractValidator<EmergencyContactUpdateDto>
    {
        public EmergencyContactUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Emergency contact name is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.MobilePhone)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Mobile phone is required")
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.MobilePhone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required");
        }
    }
}