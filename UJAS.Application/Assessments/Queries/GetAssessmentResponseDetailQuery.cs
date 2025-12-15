using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentResponseDetailQuery : IRequest<AssessmentResponseDetailDto>
    {
        public Guid ResponseId { get; set; }
        public bool IncludeQuestionDetails { get; set; } = true;
        public bool IncludeProctoringEvents { get; set; } = true;
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }

        public GetAssessmentResponseDetailQuery(Guid responseId)
        {
            ResponseId = responseId;
        }
    }

    public class AssessmentResponseDetailDto
    {
        public AssessmentResponseDto Response { get; set; }
        public AssessmentDetailDto Assessment { get; set; }
        public List<QuestionResponseDetailDto> QuestionResponses { get; set; } = new();
        public List<ProctoringEventDetailDto> ProctoringEvents { get; set; } = new();
        public ApplicantSummaryDto Applicant { get; set; }
        public ApplicationSummaryDto Application { get; set; }
    }

    public class QuestionResponseDetailDto
    {
        public QuestionResponseDto Response { get; set; }
        public AssessmentQuestionDto Question { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
    }

    public class ProctoringEventDetailDto : ProctoringEventDto
    {
        public string ScreenshotBase64 { get; set; }
        public string VideoClipBase64 { get; set; }
        public bool IsReviewed { get; set; }
        public string ReviewNotes { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string ReviewedBy { get; set; }
    }

    public class ApplicantSummaryDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }
    }

    public class ApplicationSummaryDto
    {
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
    }
}