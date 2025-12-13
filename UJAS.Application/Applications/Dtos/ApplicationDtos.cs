using UJAS.Core.Enums;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public int ApplicantProfileId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string PositionAppliedFor { get; set; }
        public ApplicationStatus Status { get; set; }
        public string StatusDisplay { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string RejectionReason { get; set; }
        public string WithdrawalReason { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignTrackingCode { get; set; }
        public int? CompletionTimeMinutes { get; set; }
        public int ApplicationVersion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Applicant details (for quick view)
        public string ApplicantPhone { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
        public string ApplicantCountry { get; set; }

        // Navigation properties
        public List<ApplicationAnswerDto> Answers { get; set; } = new();
        public List<ApplicationStatusHistoryDto> StatusHistory { get; set; } = new();
        public List<ApplicationCommentDto> Comments { get; set; } = new();
        public List<ApplicationAssessmentDto> Assessments { get; set; } = new();
        public List<ApplicationDocumentDto> Documents { get; set; } = new();
    }

    public class CreateApplicationDto
    {
        public int ApplicantProfileId { get; set; }
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public string PositionAppliedFor { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignTrackingCode { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public List<ApplicationAnswerDto> Answers { get; set; } = new();
        public List<ApplicationDocumentDto> Documents { get; set; } = new();
    }

    public class UpdateApplicationDto
    {
        public ApplicationStatus Status { get; set; }
        public string RejectionReason { get; set; }
        public string WithdrawalReason { get; set; }
    }

    public class ApplicationAnswerDto
    {
        public int CompanyFieldId { get; set; }
        public string FieldName { get; set; }
        public string FieldLabel { get; set; }
        public FieldType FieldType { get; set; }
        public string AnswerText { get; set; }
        public string AnswerJson { get; set; }
        public string FilePath { get; set; }
        public DateTime? AnswerDate { get; set; }
    }

    public class ApplicationStatusHistoryDto
    {
        public int Id { get; set; }
        public ApplicationStatus PreviousStatus { get; set; }
        public ApplicationStatus NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ApplicationCommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public string AuthorName { get; set; }
        public bool IsInternal { get; set; }
        public bool VisibleToApplicant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ApplicationAssessmentDto
    {
        public int Id { get; set; }
        public int AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public AssessmentStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? Score { get; set; }
        public bool IsPassed { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class ApplicationDocumentDto
    {
        public int Id { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public DateTime UploadDate { get; set; }
    }

    public class ApplicationFilterDto
    {
        public int? CompanyId { get; set; }
        public int? LocationId { get; set; }
        public int? ApplicantProfileId { get; set; }
        public ApplicationStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Position { get; set; }
        public string ReferralSource { get; set; }
        public bool? HasAssessment { get; set; }
        public bool? HasBackgroundCheck { get; set; }
    }

    public class ApplicationDashboardDto
    {
        public int TotalApplications { get; set; }
        public int NewApplications { get; set; }
        public int UnderReview { get; set; }
        public int Shortlisted { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
        public Dictionary<string, int> ApplicationsByStatus { get; set; } = new();
        public Dictionary<string, int> ApplicationsByLocation { get; set; } = new();
        public Dictionary<string, int> ApplicationsByMonth { get; set; } = new();
        public List<RecentApplicationDto> RecentApplications { get; set; } = new();
        public List<ApplicationStatusTrendDto> StatusTrends { get; set; } = new();
    }

    public class RecentApplicationDto
    {
        public int Id { get; set; }
        public string ApplicationNumber { get; set; }
        public string ApplicantName { get; set; }
        public string Position { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ApplicationStatusTrendDto
    {
        public string Date { get; set; }
        public int Submitted { get; set; }
        public int UnderReview { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
    }
}