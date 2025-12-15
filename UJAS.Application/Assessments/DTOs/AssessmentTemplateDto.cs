using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto Type { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> SkillsTested { get; set; } = new();
        public int QuestionCount { get; set; }
        public int EstimatedTimeMinutes { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public bool IsPublic { get; set; }
        public int UsageCount { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<AssessmentTemplateQuestionDto> Questions { get; set; } = new();
    }

    public class AssessmentTemplateQuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeDto Type { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<TemplateQuestionOptionDto> Options { get; set; } = new();
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
    }

    public class TemplateQuestionOptionDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public string Explanation { get; set; }
    }
}
