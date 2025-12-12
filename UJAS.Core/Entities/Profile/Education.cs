using System;
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
    public class Education : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        [Required]
        [MaxLength(200)]
        public string InstitutionName { get; set; }

        public InstitutionType InstitutionType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DegreeType DegreeType { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public string GPAScale { get; set; } = "4.0";
        public DateTime? GraduationDate { get; set; }
        public DateTime? ExpectedGraduationDate { get; set; }
        public bool IsCurrentlyAttending { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool? EducationVerificationConsent { get; set; }
    }
}