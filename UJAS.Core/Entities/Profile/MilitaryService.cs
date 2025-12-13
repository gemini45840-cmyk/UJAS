namespace UJAS.Core.Entities.Profile
{
    public class MilitaryService : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string BranchOfService { get; set; }
        public string RankAtDischarge { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public string TypeOfDischarge { get; set; }
        public string MilitaryOccupationalSpecialty { get; set; }
        public string SecurityClearanceLevel { get; set; }
        public DateTime? SecurityClearanceExpiration { get; set; }
        public string RelevantSkillsTraining { get; set; }
        public string DD214FilePath { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}