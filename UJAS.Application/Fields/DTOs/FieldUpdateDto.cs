using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldUpdateDto
    {
        public Guid FieldId { get; set; }

        // Basic properties
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public int? Order { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVisible { get; set; }
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }

        // Validation updates
        public ValidationUpdateDto Validation { get; set; }

        // Options updates
        public List<FieldOptionUpdateDto> Options { get; set; }

        // Conditional logic updates
        public ConditionalLogicUpdateDto ConditionalLogic { get; set; }

        // Privacy updates
        public PrivacyUpdateDto Privacy { get; set; }

        // Metadata
        public string UpdateReason { get; set; }
        public bool CreateNewVersion { get; set; } = false;
    }

    public class ValidationUpdateDto
    {
        public bool? Required { get; set; }
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

    public class FieldOptionUpdateDto
    {
        public Guid? Id { get; set; } // Null for new options
        public string Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ConditionalLogicUpdateDto
    {
        public bool? Enabled { get; set; }
        public ConditionalActionDto? Action { get; set; }
        public List<ConditionUpdateDto> Conditions { get; set; }
        public string LogicOperator { get; set; }
    }

    public class ConditionUpdateDto
    {
        public Guid? Id { get; set; }
        public Guid FieldId { get; set; }
        public ConditionOperatorDto? Operator { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class PrivacyUpdateDto
    {
        public PrivacyLevelDto? Level { get; set; }
        public bool? IsPII { get; set; }
        public bool? IsEEO { get; set; }
        public bool? IsGDPRSensitive { get; set; }
        public bool? IsEncrypted { get; set; }
        public bool? IsMaskedInLogs { get; set; }
        public int? RetentionDays { get; set; }
        public bool? AutoDelete { get; set; }
        public bool? Exportable { get; set; }
        public List<string> AllowedRoles { get; set; }
    }
}