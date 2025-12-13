using UJAS.Core.Entities.Company;
using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Assessment
{
    public class tAssessment : BaseEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public bool IsRequired { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Instructions { get; set; }
        public string ExternalAssessmentUrl { get; set; }

        // Navigation properties
        public virtual tCompany Company { get; set; }
        public virtual ICollection<AssessmentQuestion> Questions { get; set; } = new List<AssessmentQuestion>();
        public virtual ICollection<ApplicationAssessment> ApplicationAssessments { get; set; } = new List<ApplicationAssessment>();
    }
}