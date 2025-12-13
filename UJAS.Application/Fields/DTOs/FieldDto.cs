using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string FieldType { get; set; }
        public string FieldCategory { get; set; }
        public string Section { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public string ValidationRules { get; set; }
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string PlaceholderText { get; set; }
        public Dictionary<string, string> Options { get; set; } = new();
        public string ConditionalLogic { get; set; }
        public bool ApplyToAllLocations { get; set; }
        public bool IsCustomField { get; set; }
        public int? SystemFieldId { get; set; }
        public List<LocationFieldDto> LocationOverrides { get; set; } = new();
    }

    public class CreateFieldDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public string FieldType { get; set; }

        [Required]
        public string FieldCategory { get; set; }

        public string Section { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; } = true;
        public string ValidationRules { get; set; }
        public string DefaultValue { get; set; }
        public string HelpText { get; set; }
        public string PlaceholderText { get; set; }
        public Dictionary<string, string> Options { get; set; } = new();
        public string ConditionalLogic { get; set; }
        public bool ApplyToAllLocations { get; set; } = true;
        public List<int> LocationIds { get; set; } = new();
    }

    public class FieldValidationDto
    {
        public bool Required { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string Pattern { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public List<string> AllowedValues { get; set; } = new();
    }
}