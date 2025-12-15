using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public FieldTypeDto FieldType { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public bool IsPublic { get; set; }
        public bool IsSystemTemplate { get; set; }
        public int UsageCount { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }

        // Field configuration
        public FieldConfigurationDto Configuration { get; set; }

        // Preview
        public string PreviewHtml { get; set; }
        public string PreviewCss { get; set; }
        public string PreviewJs { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }

    public class FieldTemplateDetailDto : FieldTemplateDto
    {
        public FieldDto FieldDefinition { get; set; }
        public List<FieldTemplateVariantDto> Variants { get; set; } = new();
        public List<FieldTemplateExampleDto> Examples { get; set; } = new();
        public List<FieldTemplateReviewDto> Reviews { get; set; } = new();
    }

    public class FieldTemplateVariantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldTypeDto FieldType { get; set; }
        public FieldConfigurationDto Configuration { get; set; }
        public bool IsDefault { get; set; }
    }

    public class FieldTemplateExampleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExampleHtml { get; set; }
        public string ExampleData { get; set; }
        public int Order { get; set; }
    }

    public class FieldTemplateReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsVerified { get; set; }
    }

    public class FieldConfigurationDto
    {
        public Dictionary<string, object> Settings { get; set; } = new();
        public List<FieldOptionDto> Options { get; set; } = new();
        public ValidationDto Validation { get; set; }
        public ConditionalLogicDto ConditionalLogic { get; set; }
        public PrivacyDto Privacy { get; set; }
        public Dictionary<string, string> Labels { get; set; } = new();
        public Dictionary<string, string> Placeholders { get; set; } = new();
        public Dictionary<string, string> HelpTexts { get; set; } = new();
    }
}