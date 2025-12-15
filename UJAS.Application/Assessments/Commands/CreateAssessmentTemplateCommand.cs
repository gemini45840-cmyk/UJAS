using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Commands
{
    public class CreateAssessmentTemplateCommand : IRequest<Guid>
    {
        public AssessmentTemplateCreateDto Template { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? CompanyId { get; set; } // Null for public templates
        public bool IsPublic { get; set; } = false;

        public CreateAssessmentTemplateCommand(AssessmentTemplateCreateDto template, Guid createdBy)
        {
            Template = template;
            CreatedBy = createdBy;
        }
    }

    public class AssessmentTemplateCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto Type { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> SkillsTested { get; set; } = new();
        public List<TemplateQuestionCreateDto> Questions { get; set; } = new();
        public bool IsPublic { get; set; } = false;
    }

    public class TemplateQuestionCreateDto
    {
        public string QuestionText { get; set; }
        public QuestionTypeDto Type { get; set; }
        public string Category { get; set; }
        public string Difficulty { get; set; }
        public List<string> Tags { get; set; } = new();
        public List<TemplateQuestionOptionCreateDto> Options { get; set; } = new();
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; } = 1;
    }

    public class TemplateQuestionOptionCreateDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public string Explanation { get; set; }
    }
}