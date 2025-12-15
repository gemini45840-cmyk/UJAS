using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Validators
{
    public class FieldUpdateDtoValidator : AbstractValidator<FieldUpdateDto>
    {
        public FieldUpdateDtoValidator()
        {
            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Field ID is required");

            RuleFor(x => x.DisplayName)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.DisplayName))
                .WithMessage("Display name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Section)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Section))
                .WithMessage("Section cannot exceed 50 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).When(x => x.Order.HasValue)
                .WithMessage("Order must be 0 or greater");

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
            });

            RuleForEach(x => x.Options)
                .SetValidator(new FieldOptionUpdateDtoValidator()).When(x => x.Options != null);

            When(x => x.Privacy != null, () =>
            {
                RuleFor(x => x.Privacy.RetentionDays)
                    .InclusiveBetween(1, 3650).When(x => x.Privacy.RetentionDays.HasValue)
                    .WithMessage("Retention days must be between 1 and 3650");

                RuleForEach(x => x.Privacy.AllowedRoles)
                    .MaximumLength(50).When(x => x.Privacy.AllowedRoles != null)
                    .WithMessage("Allowed role cannot exceed 50 characters");
            });
        }
    }

    public class FieldOptionUpdateDtoValidator : AbstractValidator<FieldOptionUpdateDto>
    {
        public FieldOptionUpdateDtoValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Option value is required for new options")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Value))
                .WithMessage("Option value cannot exceed 100 characters");

            RuleFor(x => x.Label)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Option label is required for new options")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Label))
                .WithMessage("Option label cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 200 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater");
        }
    }

    public class ConditionUpdateDtoValidator : AbstractValidator<ConditionUpdateDto>
    {
        public ConditionUpdateDtoValidator()
        {
            RuleFor(x => x.FieldId)
                .NotEmpty().When(x => string.IsNullOrEmpty(x.Id?.ToString()))
                .WithMessage("Field ID is required");

            RuleFor(x => x.Value)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Value))
                .WithMessage("Value cannot exceed 500 characters");

            RuleForEach(x => x.Values)
                .MaximumLength(500).When(x => x.Values != null)
                .WithMessage("Value cannot exceed 500 characters");
        }
    }
}
