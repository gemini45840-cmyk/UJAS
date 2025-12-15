using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Validators
{
    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name cannot exceed 200 characters");

            RuleFor(x => x.LegalName)
                .NotEmpty().WithMessage("Legal name is required")
                .MaximumLength(200).WithMessage("Legal name cannot exceed 200 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Website)
                .MaximumLength(200).WithMessage("Website cannot exceed 200 characters")
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Valid website URL is required");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters")
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.TaxId)
                .MaximumLength(50).WithMessage("Tax ID cannot exceed 50 characters");

            RuleFor(x => x.Industry)
                .MaximumLength(100).WithMessage("Industry cannot exceed 100 characters");

            RuleFor(x => x.EmployeeCount)
                .GreaterThanOrEqualTo(1).WithMessage("Employee count must be at least 1");

            RuleFor(x => x.SizeCategory)
                .Must(x => new[] { "Small", "Medium", "Large", "Enterprise" }.Contains(x))
                .WithMessage("Size category must be Small, Medium, Large, or Enterprise");

            RuleFor(x => x.HeadquartersAddress)
                .NotNull().WithMessage("Headquarters address is required")
                .SetValidator(new AddressCreateDtoValidator());

            When(x => !x.IsMailingSameAsHeadquarters, () =>
            {
                RuleFor(x => x.MailingAddress)
                    .NotNull().WithMessage("Mailing address is required when different from headquarters")
                    .SetValidator(new AddressCreateDtoValidator());
            });

            RuleFor(x => x.InitialAdministrator)
                .NotNull().WithMessage("Initial administrator is required")
                .SetValidator(new CompanyAdministratorCreateDtoValidator());

            RuleFor(x => x.Subscription)
                .NotNull().WithMessage("Subscription information is required")
                .SetValidator(new SubscriptionCreateDtoValidator());
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    public class AddressCreateDtoValidator : AbstractValidator<AddressCreateDto>
    {
        public AddressCreateDtoValidator()
        {
            RuleFor(x => x.AddressLine1)
                .NotEmpty().WithMessage("Address line 1 is required")
                .MaximumLength(100).WithMessage("Address line 1 cannot exceed 100 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters");

            RuleFor(x => x.StateProvince)
                .NotEmpty().WithMessage("State/Province is required")
                .MaximumLength(50).WithMessage("State/Province cannot exceed 50 characters");

            RuleFor(x => x.ZipPostalCode)
                .NotEmpty().WithMessage("ZIP/Postal code is required")
                .MaximumLength(20).WithMessage("ZIP/Postal code cannot exceed 20 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required");

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");
        }
    }

    public class CompanyAdministratorCreateDtoValidator : AbstractValidator<CompanyAdministratorCreateDto>
    {
        public CompanyAdministratorCreateDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("At least one permission is required");
        }
    }

    public class SubscriptionCreateDtoValidator : AbstractValidator<SubscriptionCreateDto>
    {
        public SubscriptionCreateDtoValidator()
        {
            RuleFor(x => x.Plan)
                .NotEmpty().WithMessage("Plan is required")
                .Must(x => new[] { "Free", "Basic", "Pro", "Enterprise" }.Contains(x))
                .WithMessage("Plan must be Free, Basic, Pro, or Enterprise");

            RuleFor(x => x.BillingCycle)
                .NotEmpty().WithMessage("Billing cycle is required")
                .Must(x => new[] { "Monthly", "Quarterly", "Annually" }.Contains(x))
                .WithMessage("Billing cycle must be Monthly, Quarterly, or Annually");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required")
                .Must(x => new[] { "CreditCard", "BankTransfer", "Invoice" }.Contains(x))
                .WithMessage("Payment method must be CreditCard, BankTransfer, or Invoice");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be a 3-letter code");
        }
    }
}
