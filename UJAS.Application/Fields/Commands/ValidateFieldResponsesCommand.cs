using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class ValidateFieldResponsesCommand : IRequest<FieldValidationSummary>
    {
        public Guid? CompanyId { get; set; }
        public Dictionary<Guid, FieldResponseDto> FieldResponses { get; set; } = new();
        public Guid? ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public bool ValidateAll { get; set; } = true;
        public bool ReturnDetailedErrors { get; set; } = true;

        public ValidateFieldResponsesCommand(Dictionary<Guid, FieldResponseDto> fieldResponses)
        {
            FieldResponses = fieldResponses;
        }
    }

    public class FieldValidationSummary
    {
        public bool IsValid { get; set; }
        public int TotalFields { get; set; }
        public int ValidFields { get; set; }
        public int InvalidFields { get; set; }
        public List<FieldValidationResultDto> ValidationResults { get; set; } = new();
        public List<CrossFieldValidationDto> CrossFieldValidations { get; set; } = new();
        public Dictionary<string, object> Summary { get; set; } = new();
        public DateTime ValidatedAt { get; set; }
    }

    public class CrossFieldValidationDto
    {
        public List<Guid> FieldIds { get; set; } = new();
        public string ValidationRule { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}