using System;
using System.Collections.Generic;
using UJAS.Core.Enums;
namespace UJAS.Core.Entities.Field
{
    public class SystemField : BaseEntity
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FieldType FieldType { get; set; }
        public FieldCategory FieldCategory { get; set; }
        public string Section { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEditableByCompany { get; set; } = true;
        public bool CanBeHiddenByCompany { get; set; } = true;
        public PrivacyLevel PrivacyLevel { get; set; }
        public string ValidationRules { get; set; } // JSON
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string PlaceholderText { get; set; }
        public string Options { get; set; } // JSON for dropdown options

        // Navigation properties
        public virtual ICollection<CompanyField> CompanyFields { get; set; } = new List<CompanyField>();
    }
}