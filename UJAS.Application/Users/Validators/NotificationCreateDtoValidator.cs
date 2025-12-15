using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class NotificationCreateDtoValidator : AbstractValidator<NotificationCreateDto>
    {
        public NotificationCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required")
                .MaximumLength(1000).WithMessage("Message cannot exceed 1000 characters");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required")
                .Must(x => new[] { "Info", "Success", "Warning", "Error", "System" }.Contains(x))
                .WithMessage("Type must be Info, Success, Warning, Error, or System");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .Must(x => new[] { "Application", "Assessment", "System", "Security" }.Contains(x))
                .WithMessage("Category must be Application, Assessment, System, or Security");

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .Must(x => new[] { "Low", "Medium", "High", "Critical" }.Contains(x))
                .WithMessage("Priority must be Low, Medium, High, or Critical");
        }
    }
}
