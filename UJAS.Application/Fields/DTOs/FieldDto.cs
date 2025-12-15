using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public FieldTypeDto Type { get; set; }
        public FieldCategoryDto Category { get; set; }
        public string Section { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemField { get; set; }
        public bool IsEditable { get; set; }
        public bool IsVisible { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }

        // Company/Global context
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsGlobal { get; set; }

        // Validation
        public ValidationDto Validation { get; set; }

        // Options for select fields
        public List<FieldOptionDto> Options { get; set; } = new();

        // Conditional logic
        public ConditionalLogicDto ConditionalLogic { get; set; }

        // Privacy & Compliance
        public PrivacyDto Privacy { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int UsageCount { get; set; }
    }

    public class FieldDetailDto : FieldDto
    {
        // Extended validation
        public List<ValidationRuleDto> ValidationRules { get; set; } = new();

        // Dependencies
        public List<FieldDependencyDto> Dependencies { get; set; } = new();
        public List<FieldDependencyDto> Dependents { get; set; } = new();

        // Usage statistics
        public FieldUsageDto Usage { get; set; }

        // Audit trail
        public List<FieldHistoryDto> History { get; set; } = new();

        // Field mappings
        public List<FieldMappingDto> Mappings { get; set; } = new();
    }

    public enum FieldTypeDto
    {
        // Text Input
        Text,
        TextArea,
        Email,
        Phone,
        Url,
        Password,

        // Numbers
        Number,
        Decimal,
        Currency,
        Percentage,

        // Dates & Times
        Date,
        Time,
        DateTime,
        Month,
        Year,

        // Selection
        Dropdown,
        MultiSelect,
        Radio,
        Checkbox,

        // Files
        File,
        Image,
        Document,

        // Specialized
        Signature,
        Rating,
        Scale,
        Matrix,
        Range,
        Color,

        // Location
        Address,
        City,
        State,
        Country,
        ZipCode,

        // Complex
        Group,
        Repeater,
        Table,

        // Custom
        Custom
    }

    public enum FieldCategoryDto
    {
        PersonalInformation,
        ContactInformation,
        Address,
        Employment,
        Education,
        Skills,
        Experience,
        Documents,
        Background,
        References,
        EmergencyContacts,
        Preferences,
        Agreements,
        Custom
    }
}
