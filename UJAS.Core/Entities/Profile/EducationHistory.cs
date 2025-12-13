using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Profile
{
    public class EducationHistory : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string InstitutionName { get; set; }
        public EducationLevel InstitutionType { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public DegreeType? DegreeCertificate { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public DateTime? GraduationDate { get; set; }
        public DateTime? ExpectedGraduationDate { get; set; }
        public bool IsCurrentlyAttending { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool? EducationVerificationConsent { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}