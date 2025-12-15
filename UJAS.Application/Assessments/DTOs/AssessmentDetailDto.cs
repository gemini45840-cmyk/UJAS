using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentDetailDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssessmentTypeDto Type { get; set; }
        public AssessmentCategoryDto Category { get; set; }
        public string Instructions { get; set; }
        public string WelcomeMessage { get; set; }
        public string CompletionMessage { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public bool RandomizeQuestions { get; set; }
        public bool ShowQuestionNumbers { get; set; }
        public bool AllowBackNavigation { get; set; }
        public bool ShowProgressBar { get; set; }
        public bool RequireFullScreen { get; set; }
        public bool EnableProctoring { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public int PassingScore { get; set; }
        public decimal Weight { get; set; }
        public int MaxAttempts { get; set; }
        public int CooldownHours { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        // Sections
        public List<AssessmentSectionDto> Sections { get; set; } = new();

        // Questions (if not organized in sections)
        public List<AssessmentQuestionDto> Questions { get; set; } = new();

        // Scoring
        public AssessmentScoringDto Scoring { get; set; }

        // Assignments
        public AssessmentAssignmentDto Assignment { get; set; }

        // Settings
        public AssessmentSettingsDto Settings { get; set; }
    }

    public class AssessmentSectionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Order { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool RandomizeQuestions { get; set; }
        public int QuestionsToShow { get; set; } // 0 = show all
        public List<AssessmentQuestionDto> Questions { get; set; } = new();
    }

    public class AssessmentQuestionDto
    {
        public Guid Id { get; set; }
        public Guid? SectionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeDto Type { get; set; }
        public string Description { get; set; }
        public string HelpText { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public int Points { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> CorrectAnswers { get; set; } = new(); // For multiple correct answers
        public string Explanation { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
        public List<QuestionValidationDto> Validations { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string Difficulty { get; set; } // Easy, Medium, Hard
        public int? TimeLimitSeconds { get; set; }
        public bool RandomizeOptions { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new(); // For custom fields
    }

    public class QuestionOptionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public bool IsCorrect { get; set; }
        public decimal Points { get; set; }
        public string Explanation { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }

    public class QuestionValidationDto
    {
        public ValidationTypeDto Type { get; set; }
        public string Rule { get; set; } // Regex, min, max, etc.
        public string ErrorMessage { get; set; }
        public bool IsRequired { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Pattern { get; set; }
    }

    public class AssessmentScoringDto
    {
        public ScoringMethodDto Method { get; set; }
        public decimal TotalPossibleScore { get; set; }
        public bool ShowScoreToCandidate { get; set; }
        public bool ShowAnswersAfterCompletion { get; set; }
        public bool ShowCorrectAnswers { get; set; }
        public bool ShowExplanation { get; set; }
        public List<ScoreBandDto> ScoreBands { get; set; } = new();
        public List<SkillMeasurementDto> SkillMeasurements { get; set; } = new();
    }

    public class AssessmentAssignmentDto
    {
        public List<Guid> PositionIds { get; set; } = new();
        public List<Guid> LocationIds { get; set; } = new();
        public List<string> JobTitles { get; set; } = new();
        public List<string> Departments { get; set; } = new();
        public AssignmentTypeDto Type { get; set; } // Automatic, Manual, Conditional
        public string AssignmentRule { get; set; } // JSON condition
        public DateTime? AssignmentStartDate { get; set; }
        public DateTime? AssignmentEndDate { get; set; }
    }

    public class AssessmentSettingsDto
    {
        public bool RequireAuthentication { get; set; }
        public bool AllowPracticeAttempts { get; set; }
        public bool SaveProgressAutomatically { get; set; }
        public int AutoSaveIntervalSeconds { get; set; } = 30;
        public bool EmailNotifications { get; set; }
        public bool EnableAccessibilityMode { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        public bool AllowLanguageSwitching { get; set; }
        public bool RecordScreen { get; set; } // For proctoring
        public bool RecordWebcam { get; set; } // For proctoring
        public bool DetectCopyPaste { get; set; }
        public bool DetectTabSwitch { get; set; }
        public string CustomCSS { get; set; }
        public string CustomJavaScript { get; set; }
    }

    public enum QuestionTypeDto
    {
        MultipleChoiceSingle,
        MultipleChoiceMultiple,
        TrueFalse,
        ShortText,
        LongText,
        Essay,
        RatingScale,
        LikertScale,
        Ranking,
        Matching,
        FillInTheBlank,
        CodeEditor,
        FileUpload,
        VideoResponse,
        AudioResponse,
        ImageSelection,
        Hotspot,
        DragAndDrop,
        DatePicker,
        Numeric,
        Email,
        Phone,
        Url,
        Matrix
    }

    public enum ValidationTypeDto
    {
        Required,
        MinLength,
        MaxLength,
        Pattern,
        MinValue,
        MaxValue,
        Email,
        Phone,
        Url,
        Custom
    }

    public enum ScoringMethodDto
    {
        SimpleSum,
        Weighted,
        PartialCredit,
        NegativeMarking,
        CustomAlgorithm
    }

    public enum AssignmentTypeDto
    {
        Automatic,
        Manual,
        Conditional
    }

    public class ScoreBandDto
    {
        public string Band { get; set; }
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Recommendation { get; set; }
    }

    public class SkillMeasurementDto
    {
        public string SkillName { get; set; }
        public List<Guid> QuestionIds { get; set; } = new();
        public decimal Weight { get; set; }
        public string Description { get; set; }
    }
}
