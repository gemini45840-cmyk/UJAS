using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Profile
{
    public class Reference : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Company { get; set; }

        [MaxLength(50)]
        public string Relationship { get; set; }

        public int? YearsKnown { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string BestTimeToContact { get; set; }
        public bool? PermissionToContact { get; set; }
    }
}