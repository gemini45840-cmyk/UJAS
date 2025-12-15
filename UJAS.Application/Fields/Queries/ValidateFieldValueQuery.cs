using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class ValidateFieldValueQuery : IRequest<FieldValidationResultDto>
    {
        public Guid FieldId { get; set; }
        public string Value { get; set; }
        public List<string> MultiSelectValues { get; set; } = new();
        public Dictionary<string, string> FileValues { get; set; } = new();
        public Guid? ApplicationId { get; set; }
        public Guid? UserId { get; set; }

        public ValidateFieldValueQuery(Guid fieldId, string value)
        {
            FieldId = fieldId;
            Value = value;
        }
    }
}