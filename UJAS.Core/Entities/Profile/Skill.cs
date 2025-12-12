using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Enums;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Profile
{
    public class Skill : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public SkillCategory Category { get; set; }
        public SkillProficiency? Proficiency { get; set; }
        public int? YearsOfExperience { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }

    public enum SkillCategory
    {
        Technical,
        Professional,
        Language,
        Soft
    }
}