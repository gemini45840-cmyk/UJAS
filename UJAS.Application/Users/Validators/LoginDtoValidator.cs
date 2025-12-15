using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.RecaptchaToken)
                .NotEmpty().WithMessage("reCAPTCHA verification is required");
        }
    }

    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required");

            RuleFor(x => x.RecaptchaToken)
                .NotEmpty().WithMessage("reCAPTCHA verification is required");
        }
    }

    public class VerifyEmailDtoValidator : AbstractValidator<VerifyEmailDto>
    {
        public VerifyEmailDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Verification token is required");
        }
    }

    public class ResendVerificationDtoValidator : AbstractValidator<ResendVerificationDto>
    {
        public ResendVerificationDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Valid email address is required");

            RuleFor(x => x.RecaptchaToken)
                .NotEmpty().WithMessage("reCAPTCHA verification is required");
        }
    }

    public class TwoFactorDtoValidator : AbstractValidator<TwoFactorDto>
    {
        public TwoFactorDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Two-factor code is required")
                .Length(6).WithMessage("Two-factor code must be 6 digits");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Two-factor method is required")
                .Must(x => new[] { "Email", "Phone", "Authenticator" }.Contains(x))
                .WithMessage("Two-factor method must be Email, Phone, or Authenticator");
        }
    }
}
