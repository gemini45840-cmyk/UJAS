using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto Type { get; set; }
        public AssessmentCategoryDto Category { get; set; }
        public string Instructions { get; set; }
        public string WelcomeMessage { get; set; }
        public string CompletionMessage { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRequired { get; set; } = false;
        public bool RandomizeQuestions { get; set; } = false;
        public bool ShowQuestionNumbers { get; set; } = true;
        public bool AllowBackNavigation { get; set; } = true;
        public bool ShowProgressBar { get; set; } = true;
        public bool RequireFullScreen { get; set; } = false;
        public bool EnableProctoring { get; set; } = false;
        public int? TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; } = 70;
        public decimal Weight { get; set; } = 1.0m;
        public int MaxAttempts { get; set; } = 1;
        public int CooldownHours { get; set; } = 24;
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        // Sections
        public List<AssessmentSectionCreateDto> Sections { get; set; } = new();

        // Questions (if not using sections)
        public List<AssessmentQuestionCreateDto> Questions { get; set; } = new();

        // Scoring
        public AssessmentScoringCreateDto Scoring { get; set; }

        // Assignments
        public AssessmentAssignmentCreateDto Assignment { get; set; }

        // Settings
        public AssessmentSettingsCreateDto Settings { get; set; }
    }

    public class AssessmentSectionCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Order { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool RandomizeQuestions { get; set; } = false;
        public int QuestionsToShow { get; set; } = 0;
        public List<AssessmentQuestionCreateDto> Questions { get; set; } = new();
    }

    public class AssessmentQuestionCreateDto
    {
        public string QuestionText { get; set; }
        public QuestionTypeDto Type { get; set; }
        public string Description { get; set; }
        public string HelpText { get; set; }
        public bool IsRequired { get; set; } = true;
        public int Order { get; set; }
        public int Points { get; set; } = 1;
        public string CorrectAnswer { get; set; }
        public List<string> CorrectAnswers { get; set; } = new();
        public string Explanation { get; set; }
        public List<QuestionOptionCreateDto> Options { get; set; } = new();
        public List<QuestionValidationCreateDto> Validations { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string Difficulty { get; set; } = "Medium";
        public int? TimeLimitSeconds { get; set; }
        public bool RandomizeOptions { get; set; } = false;
    }

    public class QuestionOptionCreateDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public bool IsCorrect { get; set; } = false;
        public decimal Points { get; set; } = 0;
        public string Explanation { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }

    public class QuestionValidationCreateDto
    {
        public ValidationTypeDto Type { get; set; }
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsRequired { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Pattern { get; set; }
    }

    public class AssessmentScoringCreateDto
    {
        public ScoringMethodDto Method { get; set; } = ScoringMethodDto.SimpleSum;
        public bool ShowScoreToCandidate { get; set; } = false;
        public bool ShowAnswersAfterCompletion { get; set; } = false;
        public bool ShowCorrectAnswers { get; set; } = false;
        public bool ShowExplanation { get; set; } = false;
        public List<ScoreBandCreateDto> ScoreBands { get; set; } = new();
        public List<SkillMeasurementCreateDto> SkillMeasurements { get; set; } = new();
    }

    public class AssessmentAssignmentCreateDto
    {
        public List<Guid> PositionIds { get; set; } = new();
        public List<Guid> LocationIds { get; set; } = new();
        public List<string> JobTitles { get; set; } = new();
        public List<string> Departments { get; set; } = new();
        public AssignmentTypeDto Type { get; set; } = AssignmentTypeDto.Manual;
        public string AssignmentRule { get; set; }
        public DateTime? AssignmentStartDate { get; set; }
        public DateTime? AssignmentEndDate { get; set; }
    }

    public class AssessmentSettingsCreateDto
    {
        public bool RequireAuthentication { get; set; } = true;
        public bool AllowPracticeAttempts { get; set; } = false;
        public bool SaveProgressAutomatically { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;
        public bool EmailNotifications { get; set; } = true;
        public bool EnableAccessibilityMode { get; set; } = false;
        public List<string> SupportedLanguages { get; set; } = new() { "en" };
        public bool AllowLanguageSwitching { get; set; } = false;
        public bool RecordScreen { get; set; } = false;
        public bool RecordWebcam { get; set; } = false;
        public bool DetectCopyPaste { get; set; } = false;
        public bool DetectTabSwitch { get; set; } = false;
        public string CustomCSS { get; set; }
        public string CustomJavaScript { get; set; }
    }

    public class ScoreBandCreateDto
    {
        public string Band { get; set; }
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Recommendation { get; set; }
    }

    public class SkillMeasurementCreateDto
    {
        public string SkillName { get; set; }
        public List<Guid> QuestionIds { get; set; } = new();
        public decimal Weight { get; set; }
        public string Description { get; set; }
    }
}