using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentUpdateDto
    {
        public Guid AssessmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto? Type { get; set; }
        public AssessmentCategoryDto? Category { get; set; }
        public string Instructions { get; set; }
        public string WelcomeMessage { get; set; }
        public string CompletionMessage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRequired { get; set; }
        public bool? RandomizeQuestions { get; set; }
        public bool? ShowQuestionNumbers { get; set; }
        public bool? AllowBackNavigation { get; set; }
        public bool? ShowProgressBar { get; set; }
        public bool? RequireFullScreen { get; set; }
        public bool? EnableProctoring { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int? PassingScore { get; set; }
        public decimal? Weight { get; set; }
        public int? MaxAttempts { get; set; }
        public int? CooldownHours { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        // Sections
        public List<AssessmentSectionUpdateDto> Sections { get; set; }

        // Questions
        public List<AssessmentQuestionUpdateDto> Questions { get; set; }

        // Scoring
        public AssessmentScoringUpdateDto Scoring { get; set; }

        // Assignments
        public AssessmentAssignmentUpdateDto Assignment { get; set; }

        // Settings
        public AssessmentSettingsUpdateDto Settings { get; set; }
    }

    public class AssessmentSectionUpdateDto
    {
        public Guid? Id { get; set; } // Null for new sections
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Order { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int QuestionsToShow { get; set; }
        public List<AssessmentQuestionUpdateDto> Questions { get; set; } = new();
        public bool IsDeleted { get; set; } = false;
    }

    public class AssessmentQuestionUpdateDto
    {
        public Guid? Id { get; set; } // Null for new questions
        public string QuestionText { get; set; }
        public QuestionTypeDto? Type { get; set; }
        public string Description { get; set; }
        public string HelpText { get; set; }
        public bool? IsRequired { get; set; }
        public int Order { get; set; }
        public int? Points { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> CorrectAnswers { get; set; }
        public string Explanation { get; set; }
        public List<QuestionOptionUpdateDto> Options { get; set; } = new();
        public List<QuestionValidationUpdateDto> Validations { get; set; } = new();
        public List<string> Tags { get; set; }
        public string Difficulty { get; set; }
        public int? TimeLimitSeconds { get; set; }
        public bool? RandomizeOptions { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class QuestionOptionUpdateDto
    {
        public Guid? Id { get; set; } // Null for new options
        public string Text { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal? Points { get; set; }
        public string Explanation { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class QuestionValidationUpdateDto
    {
        public Guid? Id { get; set; }
        public ValidationTypeDto? Type { get; set; }
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
        public bool? IsRequired { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Pattern { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class AssessmentScoringUpdateDto
    {
        public ScoringMethodDto? Method { get; set; }
        public bool? ShowScoreToCandidate { get; set; }
        public bool? ShowAnswersAfterCompletion { get; set; }
        public bool? ShowCorrectAnswers { get; set; }
        public bool? ShowExplanation { get; set; }
        public List<ScoreBandUpdateDto> ScoreBands { get; set; }
        public List<SkillMeasurementUpdateDto> SkillMeasurements { get; set; }
    }

    public class AssessmentAssignmentUpdateDto
    {
        public List<Guid> PositionIds { get; set; }
        public List<Guid> LocationIds { get; set; }
        public List<string> JobTitles { get; set; }
        public List<string> Departments { get; set; }
        public AssignmentTypeDto? Type { get; set; }
        public string AssignmentRule { get; set; }
        public DateTime? AssignmentStartDate { get; set; }
        public DateTime? AssignmentEndDate { get; set; }
    }

    public class AssessmentSettingsUpdateDto
    {
        public bool? RequireAuthentication { get; set; }
        public bool? AllowPracticeAttempts { get; set; }
        public bool? SaveProgressAutomatically { get; set; }
        public int? AutoSaveIntervalSeconds { get; set; }
        public bool? EmailNotifications { get; set; }
        public bool? EnableAccessibilityMode { get; set; }
        public List<string> SupportedLanguages { get; set; }
        public bool? AllowLanguageSwitching { get; set; }
        public bool? RecordScreen { get; set; }
        public bool? RecordWebcam { get; set; }
        public bool? DetectCopyPaste { get; set; }
        public bool? DetectTabSwitch { get; set; }
        public string CustomCSS { get; set; }
        public string CustomJavaScript { get; set; }
    }

    public class ScoreBandUpdateDto
    {
        public Guid? Id { get; set; }
        public string Band { get; set; }
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Recommendation { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public class SkillMeasurementUpdateDto
    {
        public Guid? Id { get; set; }
        public string SkillName { get; set; }
        public List<Guid> QuestionIds { get; set; } = new();
        public decimal Weight { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}