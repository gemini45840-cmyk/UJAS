namespace UJAS.Core.Entities.Profile
{
    public class WorkExperience : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerStateProvince { get; set; }
        public string EmployerCountry { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentEmployer { get; set; }
        public string ReasonForLeaving { get; set; }
        public decimal? StartingSalary { get; set; }
        public decimal? EndingSalary { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string SupervisorContact { get; set; }
        public bool? CanContactEmployer { get; set; }
        public string JobResponsibilities { get; set; }
        public string KeyAccomplishments { get; set; }
        public string SkillsUtilized { get; set; }
        public string EquipmentSoftwareUsed { get; set; }
        public int? NumberOfEmployeesSupervised { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}