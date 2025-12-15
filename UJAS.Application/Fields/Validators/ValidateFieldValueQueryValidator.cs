using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Fields.Queries;

namespace UJAS.Application.Fields.Validators
{
    public class ValidateFieldValueQueryValidator : AbstractValidator<ValidateFieldValueQuery>
    {
        public ValidateFieldValueQueryValidator()
        {
            RuleFor(x => x.FieldId)
                .NotEmpty().WithMessage("Field ID is required");

            RuleFor(x => x.Value)
                .MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Value))
                .WithMessage("Value cannot exceed 4000 characters");

            RuleForEach(x => x.MultiSelectValues)
                .MaximumLength(500).When(x => x.MultiSelectValues != null)
                .WithMessage("Multi-select value cannot exceed 500 characters");
        }
    }
}