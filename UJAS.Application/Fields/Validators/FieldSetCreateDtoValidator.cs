using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Validators
{
    public class FieldSetCreateDtoValidator : AbstractValidator<FieldSetCreateDto>
    {
        public FieldSetCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field set name is required")
                .MaximumLength(100).WithMessage("Field set name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be 0 or greater");

            RuleFor(x => x.FieldIds)
                .Must(x => x.Count > 0 || (x.NewFields != null && x.NewFields.Count > 0))
                .WithMessage("At least one field or new field definition is required");

            RuleForEach(x => x.NewFields)
                .SetValidator(new FieldCreateDtoValidator()).When(x => x.NewFields != null);

            When(x => x.Settings != null, () =>
            {
                RuleFor(x => x.Settings.Layout)
                    .Must(x => string.IsNullOrEmpty(x) || new[] { "Vertical", "Horizontal", "Grid" }.Contains(x))
                    .WithMessage("Layout must be Vertical, Horizontal, or Grid");

                RuleFor(x => x.Settings.Columns)
                    .InclusiveBetween(1, 4).When(x => x.Settings.Columns > 0)
                    .WithMessage("Columns must be between 1 and 4");
            });
        }
    }
}
