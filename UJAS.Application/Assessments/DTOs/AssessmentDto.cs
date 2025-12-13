using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssessmentType { get; set; }
        public bool IsRequired { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public int MaxAttempts { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Instructions { get; set; }
        public string ExternalAssessmentUrl { get; set; }
        public List<AssessmentQuestionDto> Questions { get; set; } = new();
        public int QuestionCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AssessmentQuestionDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public Dictionary<string, string> Options { get; set; } = new();
        public string CorrectAnswer { get; set; }
        public int Points { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public string HelpText { get; set; }
    }

    public class StartAssessmentDto
    {
        public int AssessmentId { get; set; }
    }

    public class SubmitAssessmentDto
    {
        public int AssessmentId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new(); // QuestionId -> Answer
        public TimeSpan? TimeTaken { get; set; }
    }
}