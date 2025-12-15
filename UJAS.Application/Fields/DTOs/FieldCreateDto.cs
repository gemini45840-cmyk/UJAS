using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldCreateDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public FieldTypeDto Type { get; set; }
        public FieldCategoryDto Category { get; set; }
        public string Section { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; } = true;
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }

        // Company context
        public Guid? CompanyId { get; set; }

        // Validation
        public ValidationCreateDto Validation { get; set; }

        // Options for select fields
        public List<FieldOptionCreateDto> Options { get; set; } = new();

        // Conditional logic
        public ConditionalLogicCreateDto ConditionalLogic { get; set; }

        // Privacy & Compliance
        public PrivacyCreateDto Privacy { get; set; }

        // Template
        public Guid? TemplateId { get; set; }
        public bool UseTemplateDefaults { get; set; } = true;
    }

    public class ValidationCreateDto
    {
        public bool Required { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string Pattern { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public string CustomValidation { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FieldOptionCreateDto
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }

    public class ConditionalLogicCreateDto
    {
        public bool Enabled { get; set; }
        public ConditionalActionDto Action { get; set; }
        public List<ConditionCreateDto> Conditions { get; set; } = new();
        public string LogicOperator { get; set; } = "AND";
    }

    public class ConditionCreateDto
    {
        public Guid FieldId { get; set; }
        public ConditionOperatorDto Operator { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; } = new();
    }

    public class PrivacyCreateDto
    {
        public PrivacyLevelDto Level { get; set; } = PrivacyLevelDto.Internal;
        public bool IsPII { get; set; }
        public bool IsEEO { get; set; }
        public bool IsGDPRSensitive { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsMaskedInLogs { get; set; }
        public int RetentionDays { get; set; } = 365;
        public bool AutoDelete { get; set; }
        public bool Exportable { get; set; } = true;
        public List<string> AllowedRoles { get; set; } = new();
    }
}
