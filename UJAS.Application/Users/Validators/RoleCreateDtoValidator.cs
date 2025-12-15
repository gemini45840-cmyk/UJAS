using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class RoleCreateDtoValidator : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required")
                .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z][A-Za-z0-9_\s]*$").WithMessage("Role name must start with a letter and can only contain letters, numbers, underscores, and spaces");

            RuleFor(x => x.Description)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.RoleType)
                .NotEmpty().WithMessage("Role type is required")
                .Must(x => new[] { "System", "Company", "Location" }.Contains(x))
                .WithMessage("Role type must be System, Company, or Location");

            When(x => x.RoleType == "Company", () =>
            {
                RuleFor(x => x.CompanyId)
                    .NotEmpty().WithMessage("Company ID is required for company roles");
            });

            When(x => x.RoleType == "Location", () =>
            {
                RuleFor(x => x.CompanyId)
                    .NotEmpty().WithMessage("Company ID is required for location roles");
                RuleFor(x => x.LocationId)
                    .NotEmpty().WithMessage("Location ID is required for location roles");
            });

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required");

            RuleForEach(x => x.Permissions)
                .MaximumLength(100).WithMessage("Permission cannot exceed 100 characters");
        }
    }

    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required");

            RuleFor(x => x.Name)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Role name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z][A-Za-z0-9_\s]*$").When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Role name must start with a letter and can only contain letters, numbers, underscores, and spaces");

            RuleFor(x => x.Description)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("Updated by user ID is required");

            RuleForEach(x => x.AddPermissions)
                .MaximumLength(100).When(x => x.AddPermissions != null)
                .WithMessage("Permission cannot exceed 100 characters");

            RuleForEach(x => x.RemovePermissions)
                .MaximumLength(100).When(x => x.RemovePermissions != null)
                .WithMessage("Permission cannot exceed 100 characters");
        }
    }
}