using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Queries
{
    public class GetAssessmentForApplicantQuery : IRequest<ApplicantAssessmentDto>
    {
        public Guid AssessmentId { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
        public int? AttemptNumber { get; set; }
        public bool IncludePreviousResponses { get; set; } = false;

        public GetAssessmentForApplicantQuery(Guid assessmentId, Guid applicantId)
        {
            AssessmentId = assessmentId;
            ApplicantId = applicantId;
        }
    }

    public class ApplicantAssessmentDto
    {
        public AssessmentDetailDto Assessment { get; set; }
        public AssessmentResponseDto CurrentResponse { get; set; }
        public List<AssessmentResponseDto> PreviousResponses { get; set; } = new();
        public int MaxAttempts { get; set; }
        public int RemainingAttempts { get; set; }
        public DateTime? NextAttemptAllowed { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsExpired { get; set; }
        public bool CanStartNewAttempt { get; set; }
        public bool CanContinueExistingAttempt { get; set; }
        public string Instructions { get; set; }
        public List<string> ProctoringRequirements { get; set; } = new();
        public Dictionary<string, object> Settings { get; set; } = new();
    }
}