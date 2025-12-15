using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldSetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool IsSystemSet { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }

        // Fields in the set
        public List<FieldDto> Fields { get; set; } = new();

        // Settings
        public FieldSetSettingsDto Settings { get; set; }

        // Statistics
        public int FieldCount { get; set; }
        public int UsageCount { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }

    public class FieldSetDetailDto : FieldSetDto
    {
        public List<FieldSetHistoryDto> History { get; set; } = new();
        public List<FieldSetMappingDto> Mappings { get; set; } = new();
        public List<FieldSetUsageDto> UsageStatistics { get; set; } = new();
    }

    public class FieldSetSettingsDto
    {
        public bool ShowTitle { get; set; } = true;
        public bool Collapsible { get; set; } = false;
        public bool CollapsedByDefault { get; set; } = false;
        public bool AllowReordering { get; set; } = true;
        public bool ShowProgress { get; set; } = true;
        public string Layout { get; set; } = "Vertical"; // Vertical, Horizontal, Grid
        public int Columns { get; set; } = 1;
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
        public Dictionary<string, object> CustomSettings { get; set; } = new();
    }

    public class FieldSetHistoryDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public Guid ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public DateTime ChangedDate { get; set; }
    }

    public class FieldSetMappingDto
    {
        public string SystemName { get; set; }
        public string ExternalSystem { get; set; }
        public string ExternalSetName { get; set; }
        public Dictionary<string, string> FieldMappings { get; set; } = new();
        public bool IsActive { get; set; }
    }

    public class FieldSetUsageDto
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationType { get; set; }
        public DateTime UsedDate { get; set; }
        public int CompletionTimeSeconds { get; set; }
        public bool Completed { get; set; }
    }
}
