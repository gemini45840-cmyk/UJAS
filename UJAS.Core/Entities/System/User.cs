using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Enums;
using UJAS.Core.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.System
{
    public class User : BaseEntity
    {
        // Authentication
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Role and Permissions
        public RoleType Role { get; set; }
        public string Permissions { get; set; } // JSON array of Permission enum values

        // Relationships
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        // For Managers/Regional Managers
        public int? ManagedLocationId { get; set; }
        public virtual Location ManagedLocation { get; set; }

        public virtual ICollection<Location> RegionalManagedLocations { get; set; } = new List<Location>();

        // For Applicants
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        // Navigation Properties
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Application> ReviewedApplications { get; set; } = new List<Application>();
    }
}