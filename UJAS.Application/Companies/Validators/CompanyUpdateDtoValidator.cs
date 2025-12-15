using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Validators
{
    public class CompanyUpdateDtoValidator : AbstractValidator<CompanyUpdateDto>
    {
        public CompanyUpdateDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required");

            RuleFor(x => x.Name)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Company name cannot exceed 200 characters");

            RuleFor(x => x.LegalName)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.LegalName))
                .WithMessage("Legal name cannot exceed 200 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Website)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Website cannot exceed 200 characters")
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Valid website URL is required");

            RuleFor(x => x.Phone)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone number cannot exceed 20 characters")
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.TaxId)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.TaxId))
                .WithMessage("Tax ID cannot exceed 50 characters");

            RuleFor(x => x.Industry)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Industry))
                .WithMessage("Industry cannot exceed 100 characters");

            RuleFor(x => x.EmployeeCount)
                .GreaterThanOrEqualTo(1).When(x => x.EmployeeCount.HasValue)
                .WithMessage("Employee count must be at least 1");

            RuleFor(x => x.SizeCategory)
                .Must(x => string.IsNullOrEmpty(x) || new[] { "Small", "Medium", "Large", "Enterprise" }.Contains(x))
                .WithMessage("Size category must be Small, Medium, Large, or Enterprise");

            RuleFor(x => x.Status)
                .Must(x => string.IsNullOrEmpty(x) || new[] { "Active", "Inactive", "Suspended", "Pending" }.Contains(x))
                .WithMessage("Status must be Active, Inactive, Suspended, or Pending");

            When(x => x.HeadquartersAddress != null, () =>
            {
                RuleFor(x => x.HeadquartersAddress)
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

    public class AddressUpdateDtoValidator : AbstractValidator<AddressUpdateDto>
    {
        public AddressUpdateDtoValidator()
        {
            RuleFor(x => x.AddressLine1)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.AddressLine1))
                .WithMessage("Address line 1 cannot exceed 100 characters");

            RuleFor(x => x.City)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.City))
                .WithMessage("City cannot exceed 50 characters");

            RuleFor(x => x.StateProvince)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.StateProvince))
                .WithMessage("State/Province cannot exceed 50 characters");

            RuleFor(x => x.ZipPostalCode)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.ZipPostalCode))
                .WithMessage("ZIP/Postal code cannot exceed 20 characters");

            RuleFor(x => x.Country)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Country))
                .WithMessage("Country cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required");

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue)
                .WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue)
                .WithMessage("Longitude must be between -180 and 180");
        }
    }
}