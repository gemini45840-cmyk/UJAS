using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentResponseDto
    {
        public Guid Id { get; set; }
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public Guid ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public Guid ApplicationId { get; set; }
        public string ApplicationReference { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int AttemptNumber { get; set; } = 1;
        public DateTime StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ResponseStatusDto Status { get; set; }
        public int? TotalScore { get; set; }
        public decimal? PercentageScore { get; set; }
        public bool Passed { get; set; }
        public int TimeTakenMinutes { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsExpired { get; set; }
        public bool IsProctored { get; set; }
        public List<ProctoringEventDto> ProctoringEvents { get; set; } = new();
        public List<QuestionResponseDto> QuestionResponses { get; set; } = new();
        public Dictionary<string, decimal> SkillScores { get; set; } = new(); // Skill name -> score
        public string ScoreBand { get; set; }
        public string Recommendation { get; set; }
        public string Feedback { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public bool IsArchived { get; set; }
    }

    public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionTypeDto QuestionType { get; set; }
        public string Response { get; set; }
        public List<string> MultiSelectResponses { get; set; } = new();
        public Dictionary<string, string> FileResponses { get; set; } = new();
        public string FileUrl { get; set; }
        public string VideoUrl { get; set; }
        public string AudioUrl { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int? TimeTakenSeconds { get; set; }
        public int PointsAwarded { get; set; }
        public int MaxPoints { get; set; }
        public bool IsCorrect { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> CorrectAnswers { get; set; } = new();
        public string Explanation { get; set; }
        public string Feedback { get; set; }
        public bool IsReviewed { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string ReviewedBy { get; set; }
        public bool IsFlagged { get; set; }
        public string FlagReason { get; set; }
    }

    public class ProctoringEventDto
    {
        public DateTime Timestamp { get; set; }
        public ProctoringEventTypeDto EventType { get; set; }
        public string Description { get; set; }
        public string ScreenshotUrl { get; set; }
        public string VideoClipUrl { get; set; }
        public bool IsFlagged { get; set; }
        public string Severity { get; set; } // Low, Medium, High
    }

    public enum ResponseStatusDto
    {
        NotStarted,
        InProgress,
        Completed,
        Submitted,
        Graded,
        Expired,
        Terminated,
        UnderReview,
        Flagged
    }

    public enum ProctoringEventTypeDto
    {
        TabSwitch,
        CopyPaste,
        FaceNotDetected,
        MultipleFacesDetected,
        VoiceDetected,
        NoAudioDetected,
        ScreenCaptureDetected,
        Timeout,
        FullScreenExit,
        SystemCheckFailed,
        NetworkIssue,
        ManualReview,
        SuspiciousActivity
    }
}