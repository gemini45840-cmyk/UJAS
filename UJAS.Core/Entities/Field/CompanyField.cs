using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Company;
using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Field
{
    public class CompanyField : BaseEntity
    {
        public int CompanyId { get; set; }
        public int? SystemFieldId { get; set; }
        public bool IsCustomField { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public FieldType FieldType { get; set; }
        public FieldCategory FieldCategory { get; set; }
        public string Section { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsRequired { get; set; }
        public bool IsEnabled { get; set; } = true;
        public string ValidationRules { get; set; } // JSON
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string PlaceholderText { get; set; }
        public string Options { get; set; } // JSON for dropdown options
        public string ConditionalLogic { get; set; } // JSON for conditional display
        public int? MaxFileSize { get; set; }
        public string AllowedFileTypes { get; set; }
        public bool ApplyToAllLocations { get; set; } = true;

        // Navigation properties
        public virtual tCompany Company { get; set; }
        public virtual SystemField SystemField { get; set; }
        public virtual ICollection<LocationField> LocationFields { get; set; } = new List<LocationField>();
        public virtual ICollection<ApplicationAnswer> ApplicationAnswers { get; set; } = new List<ApplicationAnswer>();
    }
}