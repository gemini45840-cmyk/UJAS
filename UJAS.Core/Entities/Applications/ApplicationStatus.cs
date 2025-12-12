using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using UJAS.Core.Enums;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Applications
{
    public class ApplicationStatus : BaseEntity
    {
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        public ApplicationStatus Status { get; set; }
        public string ChangedByUserId { get; set; }
        public string ChangedByUserName { get; set; }
        public string Notes { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}