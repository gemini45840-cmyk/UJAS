using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Validators
{
    public class FieldCreateDtoValidator : AbstractValidator<FieldCreateDto>
    {
        public FieldCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field name is required")
                .MaximumLength(50).WithMessage("Field name cannot exceed 50 characters")
                .Matches(@"^[A-Za-z][A-Za-z0-9_]*$").WithMessage("Field name must start with a letter and can only contain letters, numbers, and underscores");

            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name is required")
                .MaximumLength(100).WithMessage("Display name cannot exceed 100 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid field type");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid field category");

            RuleFor(x => x.Section)
                .NotEmpty().WithMessage("Section is required")
                .MaximumLength(50).WithMessage("Section cannot exceed 50 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater");

            RuleFor(x => x.DataType)
                .NotEmpty().WithMessage("Data type is required")
                .Must(x => new[] { "string", "int", "decimal", "bool", "DateTime", "Guid", "object" }.Contains(x))
                .WithMessage("Invalid data type");

            RuleFor(x => x.HelpText)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.HelpText))
                .WithMessage("Help text cannot exceed 500 characters");

            RuleFor(x => x.Placeholder)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Placeholder))
                .WithMessage("Placeholder cannot exceed 200 characters");

            RuleFor(x => x.DefaultValue)
                .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.DefaultValue))
                .WithMessage("Default value cannot exceed 1000 characters");

            When(x => x.Validation != null, () =>
            {
                RuleFor(x => x.Validation.MinLength)
                    .GreaterThan(0).When(x => x.Validation.MinLength.HasValue)
                    .WithMessage("Minimum length must be greater than 0");

                RuleFor(x => x.Validation.MaxLength)
                    .GreaterThanOrEqualTo(x => x.Validation.MinLength)
                    .When(x => x.Validation.MaxLength.HasValue && x.Validation.MinLength.HasValue)
                    .WithMessage("Maximum length must be greater than or equal to minimum length");

                RuleFor(x => x.Validation.MinValue)
                    .LessThan(x => x.Validation.MaxValue)
                    .When(x => x.Validation.MinValue.HasValue && x.Validation.MaxValue.HasValue)
                    .WithMessage("Minimum value must be less than maximum value");

                RuleFor(x => x.Validation.MinDate)
                    .LessThan(x => x.Validation.MaxDate)
                    .When(x => x.Validation.MinDate.HasValue && x.Validation.MaxDate.HasValue)
                    .WithMessage("Minimum date must be before maximum date");

                RuleFor(x => x.Validation.Pattern)
                    .Must(BeValidRegex).When(x => !string.IsNullOrEmpty(x.Validation.Pattern))
                    .WithMessage("Pattern must be a valid regular expression");
            });

            RuleForEach(x => x.Options)
                .SetValidator(new FieldOptionCreateDtoValidator());

            When(x => x.ConditionalLogic != null && x.ConditionalLogic.Enabled, () =>
            {
                RuleFor(x => x.ConditionalLogic.Conditions)
                    .NotEmpty().WithMessage("At least one condition is required for conditional logic");

                RuleFor(x => x.ConditionalLogic.LogicOperator)
                    .Must(x => x == "AND" || x == "OR")
                    .WithMessage("Logic operator must be AND or OR");
            });

            When(x => x.Privacy != null, () =>
            {
                RuleFor(x => x.Privacy.RetentionDays)
                    .InclusiveBetween(1, 3650).When(x => x.Privacy.RetentionDays > 0)
                    .WithMessage("Retention days must be between 1 and 3650");

                RuleForEach(x => x.Privacy.AllowedRoles)
                    .MaximumLength(50).When(x => x.Privacy.AllowedRoles != null)
                    .WithMessage("Allowed role cannot exceed 50 characters");
            });
        }

        private bool BeValidRegex(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return true;

            try
            {
                Regex.Match("", pattern);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class FieldOptionCreateDtoValidator : AbstractValidator<FieldOptionCreateDto>
    {
        public FieldOptionCreateDtoValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Option value is required")
                .MaximumLength(100).WithMessage("Option value cannot exceed 100 characters");

            RuleFor(x => x.Label)
                .NotEmpty().WithMessage("Option label is required")
                .MaximumLength(100).WithMessage("Option label cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater");
        }
    }

    public class ConditionCreateDtoValidator : AbstractValidator<ConditionCreateDto>
    {
        public ConditionCreateDtoValidator()
        {
            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Field ID is required");

            RuleFor(x => x.Operator)
                .IsInEnum().WithMessage("Invalid condition operator");

            RuleFor(x => x.Value)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Value))
                .WithMessage("Value cannot exceed 500 characters");

            RuleForEach(x => x.Values)
                .MaximumLength(500).When(x => x.Values != null)
                .WithMessage("Value cannot exceed 500 characters");
        }
    }
}