using FluentValidation;
using UJAS.Application.Companies.Dtos;

namespace UJAS.Application.Companies.Validators
{
    public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name cannot exceed 200 characters");

            RuleFor(x => x.LegalName)
                .MaximumLength(200).WithMessage("Legal name cannot exceed 200 characters");

            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Website cannot exceed 200 characters")
                .Matches(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")
                .When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Website must be a valid URL");

            RuleFor(x => x.Industry)
                .MaximumLength(100).WithMessage("Industry cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(x => x.PrimaryColor)
                .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                .When(x => !string.IsNullOrEmpty(x.PrimaryColor))
                .WithMessage("Primary color must be a valid hex color");

            RuleFor(x => x.SecondaryColor)
                .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                .When(x => !string.IsNullOrEmpty(x.SecondaryColor))
                .WithMessage("Secondary color must be a valid hex color");

            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("Time zone is required")
                .MaximumLength(50).WithMessage("Time zone cannot exceed 50 characters");

            RuleFor(x => x.Settings)
                .NotNull().WithMessage("Company settings are required");

            When(x => x.Settings != null, () =>
            {
                RuleFor(x => x.Settings.AutoReplyMessage)
                    .MaximumLength(2000).WithMessage("Auto-reply message cannot exceed 2000 characters");

                RuleFor(x => x.Settings.DefaultLanguage)
                    .NotEmpty().WithMessage("Default language is required")
                    .MaximumLength(10).WithMessage("Default language cannot exceed 10 characters");

                RuleFor(x => x.Settings.DateFormat)
                    .NotEmpty().WithMessage("Date format is required")
                    .MaximumLength(20).WithMessage("Date format cannot exceed 20 characters");
            });
        }
    }

    public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name cannot exceed 200 characters");

            RuleFor(x => x.LegalName)
                .MaximumLength(200).WithMessage("Legal name cannot exceed 200 characters");

            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Website cannot exceed 200 characters")
                .Matches(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")
                .When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Website must be a valid URL");

            RuleFor(x => x.Industry)
                .MaximumLength(100).WithMessage("Industry cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(x => x.PrimaryColor)
                .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                .When(x => !string.IsNullOrEmpty(x.PrimaryColor))
                .WithMessage("Primary color must be a valid hex color");

            RuleFor(x => x.SecondaryColor)
                .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                .When(x => !string.IsNullOrEmpty(x.SecondaryColor))
                .WithMessage("Secondary color must be a valid hex color");

            RuleFor(x => x.TimeZone)
                .NotEmpty().WithMessage("Time zone is required")
                .MaximumLength(50).WithMessage("Time zone cannot exceed 50 characters");

            RuleFor(x => x.Settings)
                .NotNull().WithMessage("Company settings are required");

            When(x => x.Settings != null, () =>
            {
                RuleFor(x => x.Settings.AutoReplyMessage)
                    .MaximumLength(2000).WithMessage("Auto-reply message cannot exceed 2000 characters");

                RuleFor(x => x.Settings.DefaultLanguage)
                    .NotEmpty().WithMessage("Default language is required")
                    .MaximumLength(10).WithMessage("Default language cannot exceed 10 characters");

                RuleFor(x => x.Settings.DateFormat)
                    .NotEmpty().WithMessage("Date format is required")
                    .MaximumLength(20).WithMessage("Date format cannot exceed 20 characters");
            });
        }
    }
}