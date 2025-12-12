using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.System
{
    public class Location : TenantEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ValueObjects.Address Address { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        // Status
        public bool IsActive { get; set; } = true;
        public bool AcceptingApplications { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<User> Managers { get; set; } = new List<User>();
        public virtual ICollection<User> RegionalManagers { get; set; } = new List<User>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
