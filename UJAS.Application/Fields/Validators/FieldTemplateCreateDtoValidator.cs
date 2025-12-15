using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Fields.Commands;

namespace UJAS.Application.Fields.Validators
{
    public class FieldTemplateCreateDtoValidator : AbstractValidator<FieldTemplateCreateDto>
    {
        public FieldTemplateCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Template name is required")
                .MaximumLength(100).WithMessage("Template name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

            RuleFor(x => x.FieldType)
                .IsInEnum().WithMessage("Invalid field type");

            RuleFor(x => x.Industry)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Industry))
                .WithMessage("Industry cannot exceed 50 characters");

            RuleForEach(x => x.JobRoles)
                .MaximumLength(50).When(x => x.JobRoles != null)
                .WithMessage("Job role cannot exceed 50 characters");

            RuleForEach(x => x.Tags)
                .MaximumLength(50).When(x => x.Tags != null)
                .WithMessage("Tag cannot exceed 50 characters");

            RuleFor(x => x.FieldDefinition)
                .NotNull().WithMessage("Field definition is required")
                .SetValidator(new FieldCreateDtoValidator());
        }
    }
}
