using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$").WithMessage("Username must start with a letter and can only contain letters, numbers, and underscores");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z\s\-']+$").WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[+]?[\d\s\-().]+$").WithMessage("Valid phone number is required")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
                .Matches(@"(?=.*[a-z])").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"(?=.*[A-Z])").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"(?=.*\d)").WithMessage("Password must contain at least one number")
                .Matches(@"(?=.*[@$!%*?&])").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.UserType)
                .NotEmpty().WithMessage("User type is required")
                .Must(x => new[] { "SystemAdmin", "CompanyAdmin", "RegionalManager", "Manager", "Applicant" }.Contains(x))
                .WithMessage("User type must be SystemAdmin, CompanyAdmin, RegionalManager, Manager, or Applicant");

            When(x => x.UserType != "SystemAdmin" && x.UserType != "Applicant", () =>
            {
                RuleFor(x => x.CompanyId)
                    .NotEmpty().WithMessage("Company ID is required for this user type");
            });

            When(x => x.UserType == "RegionalManager" || x.UserType == "Manager", () =>
            {
                RuleFor(x => x.LocationIds)
                    .NotEmpty().WithMessage("At least one location ID is required for this user type");
            });

            RuleForEach(x => x.Permissions)
                .MaximumLength(100).WithMessage("Permission cannot exceed 100 characters");
        }
    }

    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
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

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[+]?[\d\s\-().]+$").WithMessage("Valid phone number is required")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
                .Matches(@"(?=.*[a-z])").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"(?=.*[A-Z])").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"(?=.*\d)").WithMessage("Password must contain at least one number")
                .Matches(@"(?=.*[@$!%*?&])").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.AcceptTerms)
                .Equal(true).WithMessage("You must accept the terms and conditions");

            RuleFor(x => x.AcceptPrivacyPolicy)
                .Equal(true).WithMessage("You must accept the privacy policy");

            RuleFor(x => x.RecaptchaToken)
                .NotEmpty().WithMessage("reCAPTCHA verification is required");
        }
    }
}