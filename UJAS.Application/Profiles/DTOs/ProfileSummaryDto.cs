using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.DTOs
{
    public class ProfileSummaryDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentCompany { get; set; }
        public string Location { get; set; }
        public string Summary { get; set; }
        public List<string> TopSkills { get; set; } = new();
        public List<EducationSummaryDto> EducationSummary { get; set; } = new();
        public List<ExperienceSummaryDto> ExperienceSummary { get; set; } = new();
        public bool IsProfileComplete { get; set; }
        public int CompletionPercentage { get; set; }
        public DateTime? LastActive { get; set; }
        public string ProfileUrl { get; set; }
    }

    public class EducationSummaryDto
    {
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public int? GraduationYear { get; set; }
    }

    public class ExperienceSummaryDto
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public int? YearsOfExperience { get; set; }
        public bool IsCurrent { get; set; }
    }
}
