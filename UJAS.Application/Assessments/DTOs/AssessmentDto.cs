using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto Type { get; set; }
        public AssessmentCategoryDto Category { get; set; }
        public string Instructions { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public decimal Weight { get; set; } // For scoring weight in overall application
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int QuestionCount { get; set; }
        public int AverageCompletionMinutes { get; set; }
        public decimal AverageScore { get; set; }
        public int TotalCompletions { get; set; }
        public List<string> AssignedPositions { get; set; } = new();
        public List<string> AssignedLocations { get; set; } = new();
        public List<Guid> AssignedPositionIds { get; set; } = new();
        public List<Guid> AssignedLocationIds { get; set; } = new();
    }

    public enum AssessmentTypeDto
    {
        Personality,
        Cognitive,
        Skills,
        Technical,
        Language,
        SituationalJudgment,
        Behavioral,
        Knowledge,
        Aptitude,
        VideoInterview,
        CodingChallenge,
        PortfolioReview,
        Custom
    }

    public enum AssessmentCategoryDto
    {
        PreScreening,
        MainAssessment,
        Supplemental,
        Verification
    }
}