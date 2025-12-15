using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Validators
{
    public class LocationCreateDtoValidator : AbstractValidator<LocationCreateDto>
    {
        public LocationCreateDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Location name is required")
                .MaximumLength(100).WithMessage("Location name cannot exceed 100 characters");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Location code is required")
                .MaximumLength(20).WithMessage("Location code cannot exceed 20 characters")
                .Matches(@"^[A-Za-z0-9\-_]+$").WithMessage("Location code can only contain letters, numbers, hyphens, and underscores");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Location type is required")
                .Must(x => new[] { "Headquarters", "Branch", "Store", "Warehouse", "Remote", "Other" }.Contains(x))
                .WithMessage("Location type must be Headquarters, Branch, Store, Warehouse, Remote, or Other");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Valid email address is required");

            RuleFor(x => x.Phone)
                .Matches(@"^[+]?[\d\s\-().]+$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Valid phone number is required");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required")
                .SetValidator(new AddressCreateDtoValidator());

            RuleFor(x => x.Settings)
                .SetValidator(new LocationSettingsCreateDtoValidator());

            RuleFor(x => x.InitialManager)
                .SetValidator(new LocationManagerCreateDtoValidator());
        }
    }

    public class LocationSettingsCreateDtoValidator : AbstractValidator<LocationSettingsCreateDto>
    {
        public LocationSettingsCreateDtoValidator()
        {
            RuleFor(x => x.MaxApplicationsPerDay)
                .GreaterThan(0).WithMessage("Maximum applications per day must be greater than 0");

            RuleForEach(x => x.NotificationEmails)
                .EmailAddress().When(x => x.NotificationEmails != null)
                .WithMessage("Valid email address is required");
        }
    }

    public class LocationManagerCreateDtoValidator : AbstractValidator<LocationManagerCreateDto>
    {
        public LocationManagerCreateDtoValidator()
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
}
