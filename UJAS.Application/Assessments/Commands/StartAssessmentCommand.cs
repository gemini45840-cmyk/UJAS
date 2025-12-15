using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class StartAssessmentCommand : IRequest<Guid>
    {
        public Guid AssessmentId { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid ApplicationId { get; set; }
        public int AttemptNumber { get; set; } = 1;
        public DateTime? ExpiresAt { get; set; }
        public string DeviceInfo { get; set; }
        public string BrowserInfo { get; set; }
        public string IpAddress { get; set; }
        public bool IsPracticeAttempt { get; set; } = false;

        public StartAssessmentCommand(Guid assessmentId, Guid applicantId, Guid applicationId)
        {
            AssessmentId = assessmentId;
            ApplicantId = applicantId;
            ApplicationId = applicationId;
        }
    }
}