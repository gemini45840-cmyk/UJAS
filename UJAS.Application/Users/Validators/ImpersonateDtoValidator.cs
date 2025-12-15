using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class ImpersonateDtoValidator : AbstractValidator<ImpersonateDto>
    {
        public ImpersonateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID to impersonate is required");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Impersonation reason is required")
                .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters");

            RuleFor(x => x.ExpiryTime)
                .GreaterThan(DateTime.UtcNow).When(x => x.ExpiryTime.HasValue)
                .WithMessage("Expiry time must be in the future");
        }
    }
}
