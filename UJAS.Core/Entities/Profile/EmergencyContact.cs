using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Profile
{
    public class EmergencyContact : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }

        [Required]
        public string Phone { get; set; }

        public string AlternatePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}