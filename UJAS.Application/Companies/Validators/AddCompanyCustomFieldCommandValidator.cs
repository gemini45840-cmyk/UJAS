using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Companies.Commands;

namespace UJAS.Application.Companies.Validators
{
    public class AddCompanyCustomFieldCommandValidator : AbstractValidator<AddCompanyCustomFieldCommand>
    {
        public AddCompanyCustomFieldCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("Company ID is required");

            RuleFor(x => x.FieldName)
                .NotEmpty().WithMessage("Field name is required")
                .MaximumLength(50).WithMessage("Field name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$").WithMessage("Field name must start with a letter and can only contain letters, numbers, and underscores");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name is required")
                .MaximumLength(100).WithMessage("Display name cannot exceed 100 characters");

            RuleFor(x => x.FieldType)
                .NotEmpty().WithMessage("Field type is required")
                .Must(x => new[] { "Text", "TextArea", "Number", "Date", "Dropdown", "Checkbox", "Radio", "File", "Email", "Phone", "URL", "Rating", "YesNo" }.Contains(x))
                .WithMessage("Invalid field type");

            RuleFor(x => x.DataType)
                .NotEmpty().WithMessage("Data type is required")
                .Must(x => new[] { "string", "int", "decimal", "DateTime", "bool", "Guid" }.Contains(x))
                .WithMessage("Invalid data type");

            RuleFor(x => x.Section)
                .NotEmpty().WithMessage("Section is required")
                .MaximumLength(50).WithMessage("Section cannot exceed 50 characters");

            RuleFor(x => x.HelpText)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.HelpText))
                .WithMessage("Help text cannot exceed 500 characters");

            RuleFor(x => x.Placeholder)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Placeholder))
                .WithMessage("Placeholder cannot exceed 200 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater");

            RuleFor(x => x.PrivacyLevel)
                .Must(x => new[] { "Public", "Internal", "Confidential", "Restricted" }.Contains(x))
                .WithMessage("Privacy level must be Public, Internal, Confidential, or Restricted");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("Created by user ID is required");
        }
    }
}