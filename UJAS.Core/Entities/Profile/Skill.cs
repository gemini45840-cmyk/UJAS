using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Profile
{
    public class Skill : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public SkillType SkillType { get; set; }
        public string Name { get; set; }
        public ProficiencyLevel? ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}