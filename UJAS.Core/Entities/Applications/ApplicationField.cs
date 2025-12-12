using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.System;
using UJAS.Core.Enums;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Applications
{
    public class ApplicationField : TenantEntity
    {
        // Field Identification
        [Required]
        [MaxLength(100)]
        public string FieldKey { get; set; } // Unique identifier for the field

        [Required]
        [MaxLength(200)]
        public string Label { get; set; }

        public FieldType FieldType { get; set; }
        public FieldCategory Category { get; set; }

        // Configuration
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEditable { get; set; } = true;
        public bool IsGlobalDefault { get; set; }
        public bool IsCompanyCustom { get; set; }

        // Validation
        public string ValidationRegex { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string AllowedFileTypes { get; set; } // Comma-separated: .pdf,.doc,.docx
        public long? MaxFileSize { get; set; } // In bytes

        // Options for dropdowns/multiselect
        public string OptionsJson { get; set; } // JSON array of options

        // Privacy & Compliance
        public PrivacyFlag? PrivacyFlag { get; set; }
        public bool IsPII { get; set; }
        public bool IsEEO { get; set; }
        public int? RetentionDays { get; set; }
        public bool RequiresEncryption { get; set; }

        // Help & Placeholder
        public string Placeholder { get; set; }
        public string HelpText { get; set; }

        // Conditional Logic
        public string ConditionalLogicJson { get; set; } // JSON for show/hide conditions

        // Location/Position Specific
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string PositionTypes { get; set; } // Comma-separated position types
    }
}
