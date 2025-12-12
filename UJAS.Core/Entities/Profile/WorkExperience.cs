using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Profile
{
    public class WorkExperience : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmployerName { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
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
        public bool? ContactEmployerPermission { get; set; }
        public string Responsibilities { get; set; }
        public string Accomplishments { get; set; }
        public string SkillsUsed { get; set; }
        public int? EmployeesSupervised { get; set; }
    }
}
