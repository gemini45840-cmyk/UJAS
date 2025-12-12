using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using UJAS.Core.Enums;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.System
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; } = "#3B82F6";
        public string SecondaryColor { get; set; } = "#1E40AF";

        // Widget Configuration
        public bool IsWidgetActive { get; set; } = true;
        public string WidgetEmbedCode { get; set; }
        public bool ShowLogoInWidget { get; set; } = true;

        // Application Settings
        public bool AllowAutoSave { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;
        public int ApplicationRetentionDays { get; set; } = 365;
        public bool StoreApplicantProfiles { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<ApplicationField> CustomFields { get; set; } = new List<ApplicationField>();
        public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public virtual ICollection<WidgetConfiguration> WidgetConfigurations { get; set; } = new List<WidgetConfiguration>();
    }
}