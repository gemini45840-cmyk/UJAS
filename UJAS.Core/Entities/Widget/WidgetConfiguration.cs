using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Widget
{
    public class WidgetConfiguration : TenantEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Appearance
        public string PrimaryColor { get; set; }
        public string ButtonColor { get; set; }
        public string TextColor { get; set; }
        public string FontFamily { get; set; } = "Arial, sans-serif";
        public int FontSize { get; set; } = 14;
        public bool ShowCompanyLogo { get; set; } = true;
        public string LogoPosition { get; set; } = "top-left";

        // Behavior
        public WidgetTrigger Trigger { get; set; } = WidgetTrigger.ButtonClick;
        public string TriggerElementId { get; set; }
        public int DelaySeconds { get; set; } = 0;
        public bool ShowOnMobile { get; set; } = true;
        public bool ShowOnDesktop { get; set; } = true;

        // Content
        public string WelcomeMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string CallToAction { get; set; } = "Apply Now";

        // Fields Configuration
        public string VisibleFieldsJson { get; set; } // JSON array of field keys to show
        public string RequiredFieldsJson { get; set; } // JSON array of required field keys

        // Analytics
        public string TrackingCode { get; set; }
        public bool TrackConversions { get; set; } = true;

        // Status
        public bool IsActive { get; set; } = true;
        public DateTime? ActivatedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<EmbedCode> EmbedCodes { get; set; } = new List<EmbedCode>();
    }

    public enum WidgetTrigger
    {
        ButtonClick,
        PageLoad,
        ScrollPercentage,
        ExitIntent,
        TimeDelay
    }
}