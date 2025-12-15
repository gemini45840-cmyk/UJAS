using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class CreateFieldTemplateCommand : IRequest<Guid>
    {
        public FieldTemplateCreateDto Template { get; set; }
        public Guid CreatedBy { get; set; }

        public CreateFieldTemplateCommand(FieldTemplateCreateDto template, Guid createdBy)
        {
            Template = template;
            CreatedBy = createdBy;
        }
    }

    public class FieldTemplateCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public FieldTypeDto FieldType { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public bool IsPublic { get; set; } = false;

        // Field configuration
        public FieldCreateDto FieldDefinition { get; set; }

        // Preview
        public string PreviewHtml { get; set; }
        public string PreviewCss { get; set; }
        public string PreviewJs { get; set; }
    }
}