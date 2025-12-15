using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class WorkExperienceDto
    {
        public Guid Id { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
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
        public bool MayContactEmployer { get; set; }
        public string JobResponsibilities { get; set; }
        public List<string> KeyAccomplishments { get; set; } = new();
        public List<string> SkillsUtilized { get; set; } = new();
        public List<string> EquipmentSoftwareUsed { get; set; } = new();
        public int? NumberOfEmployeesSupervised { get; set; }

        // Military Service (if applicable)
        public string BranchOfService { get; set; }
        public string RankAtDischarge { get; set; }
        public DateTime? MilitaryStartDate { get; set; }
        public DateTime? MilitaryEndDate { get; set; }
        public string TypeOfDischarge { get; set; }
        public string MilitaryOccupationalSpecialty { get; set; }
        public string SecurityClearanceLevel { get; set; }
        public DateTime? SecurityClearanceExpiration { get; set; }
        public string RelevantMilitarySkills { get; set; }
    }
}