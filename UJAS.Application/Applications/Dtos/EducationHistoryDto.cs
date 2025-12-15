using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class EducationHistoryDto
    {
        public Guid Id { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionType { get; set; }
        public string Location { get; set; }
        public string DegreeCertificate { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public DateTime? GraduationDate { get; set; }
        public DateTime? ExpectedGraduationDate { get; set; }
        public bool IsCurrentlyAttending { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool EducationVerificationConsent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}