using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.System;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Applications
{
    public class ApplicationComment : BaseEntity
    {
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string Comment { get; set; }

        public bool IsVisibleToApplicant { get; set; }
        public bool IsInternalNote { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}