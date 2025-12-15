using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class ValidationDto
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
        public string WarningMessage { get; set; }
    }

    public class ValidationRuleDto
    {
        public Guid Id { get; set; }
        public ValidationRuleTypeDto Type { get; set; }
        public string Rule { get; set; } // JSON or expression
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }

    public class FieldOptionDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }

    public class ConditionalLogicDto
    {
        public bool Enabled { get; set; }
        public ConditionalActionDto Action { get; set; }
        public List<ConditionDto> Conditions { get; set; } = new();
        public string LogicOperator { get; set; } // AND, OR
    }

    public class ConditionDto
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public ConditionOperatorDto Operator { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; } = new();
    }

    public class PrivacyDto
    {
        public PrivacyLevelDto Level { get; set; }
        public bool IsPII { get; set; }
        public bool IsEEO { get; set; }
        public bool IsGDPRSensitive { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsMaskedInLogs { get; set; }
        public int RetentionDays { get; set; }
        public bool AutoDelete { get; set; }
        public bool Exportable { get; set; }
        public List<string> AllowedRoles { get; set; } = new();
    }

    public class FieldDependencyDto
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public string DependencyType { get; set; } // Requires, Hides, Shows, Enables, Disables
        public string Condition { get; set; }
        public bool IsBidirectional { get; set; }
    }

    public class FieldUsageDto
    {
        public int TotalApplications { get; set; }
        public int CompletedApplications { get; set; }
        public int SkippedCount { get; set; }
        public int ErrorCount { get; set; }
        public decimal AverageCompletionTimeSeconds { get; set; }
        public decimal CompletionRate { get; set; }
        public List<string> CommonValues { get; set; } = new();
        public Dictionary<string, int> OptionUsage { get; set; } = new();
    }

    public class FieldHistoryDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public Guid ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public DateTime ChangedDate { get; set; }
        public string Reason { get; set; }
    }

    public class FieldMappingDto
    {
        public string SystemName { get; set; }
        public string ExternalSystem { get; set; }
        public string ExternalFieldName { get; set; }
        public string MappingRule { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastSynced { get; set; }
    }

    public enum ValidationRuleTypeDto
    {
        Required,
        Length,
        Pattern,
        Range,
        DateRange,
        Email,
        Phone,
        Url,
        Custom,
        CrossField
    }

    public enum ConditionalActionDto
    {
        Show,
        Hide,
        Enable,
        Disable,
        Require,
        MakeOptional,
        SetValue,
        ClearValue
    }

    public enum ConditionOperatorDto
    {
        Equals,
        NotEquals,
        Contains,
        NotContains,
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        IsEmpty,
        IsNotEmpty,
        IsChecked,
        IsNotChecked,
        In,
        NotIn
    }

    public enum PrivacyLevelDto
    {
        Public,
        Internal,
        Confidential,
        Restricted
    }
}
