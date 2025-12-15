using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UJAS.Application.Profiles.Commands;

namespace UJAS.Application.Profiles.Validators
{
    public class UploadProfileDocumentCommandValidator : AbstractValidator<UploadProfileDocumentCommand>
    {
        public UploadProfileDocumentCommandValidator()
        {
            RuleFor(x => x.ProfileId)
                .NotEmpty().WithMessage("Profile ID is required");

            RuleFor(x => x.DocumentType)
                .NotEmpty().WithMessage("Document type is required")
                .Must(x => new[] { "Resume", "CoverLetter", "Transcript", "Diploma", "Certificate", "Portfolio", "Photo", "Other" }.Contains(x))
                .WithMessage("Invalid document type");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File name is required")
                .MaximumLength(200).WithMessage("File name cannot exceed 200 characters")
                .Must(x => x.Contains('.')).WithMessage("File name must have an extension");

            RuleFor(x => x.FileContentBase64)
                .NotEmpty().WithMessage("File content is required")
                .Must(BeValidBase64).WithMessage("File content must be valid base64");

            RuleFor(x => x.FileType)
                .NotEmpty().WithMessage("File type is required")
                .Must(x => new[] { "pdf", "doc", "docx", "txt", "rtf", "jpg", "jpeg", "png", "gif" }.Contains(x.ToLower()))
                .WithMessage("Unsupported file type");

            RuleFor(x => x.UploadedBy)
                .NotEmpty().WithMessage("Uploaded by user ID is required");
        }

        private bool BeValidBase64(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return false;

            try
            {
                // Try to convert to byte array
                Convert.FromBase64String(base64);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
