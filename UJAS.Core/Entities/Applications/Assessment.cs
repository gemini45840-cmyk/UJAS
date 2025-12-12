using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Applications
{
    public class Assessment : TenantEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }
        public AssessmentType Type { get; set; }
        public AssessmentStatus Status { get; set; } = AssessmentStatus.Active;

        // Configuration
        public int TimeLimitMinutes { get; set; } = 30;
        public int PassingScore { get; set; } = 70;
        public bool IsRequired { get; set; }
        public bool AllowRetake { get; set; }
        public int RetakeDelayDays { get; set; } = 30;
        public int MaxAttempts { get; set; } = 3;

        // Questions
        public string QuestionsJson { get; set; } // JSON array of questions

        // Scheduling
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }

        // Integration
        public string ExternalAssessmentId { get; set; }
        public string IntegrationUrl { get; set; }

        // Navigation Properties
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }

    public enum AssessmentType
    {
        Personality,
        SkillsTest,
        Cognitive,
        SituationalJudgement,
        VideoInterview,
        CodingChallenge,
        Custom
    }

    public enum AssessmentStatus
    {
        Draft,
        Active,
        Inactive,
        Archived
    }
}