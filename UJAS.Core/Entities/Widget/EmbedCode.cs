using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.System;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Widget
{
    public class EmbedCode : BaseEntity
    {
        public int WidgetConfigurationId { get; set; }
        public virtual WidgetConfiguration WidgetConfiguration { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        public string Code { get; set; } // The actual JavaScript code

        [Required]
        [MaxLength(50)]
        public string Version { get; set; }

        public EmbedCodeStatus Status { get; set; } = EmbedCodeStatus.Active;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public int UsageCount { get; set; }

        // Security
        public string ApiKey { get; set; }
        public string SecretHash { get; set; }
        public string AllowedDomains { get; set; } // Comma-separated list
        public bool RequireHTTPS { get; set; } = true;
    }

    public enum EmbedCodeStatus
    {
        Active,
        Inactive,
        Revoked,
        Expired
    }
}