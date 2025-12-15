using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class SubmitAssessmentCommand : IRequest<bool>
    {
        public Guid ResponseId { get; set; }
        public Guid ApplicantId { get; set; }
        public bool ForceSubmit { get; set; } = false;

        public SubmitAssessmentCommand(Guid responseId, Guid applicantId)
        {
            ResponseId = responseId;
            ApplicantId = applicantId;
        }
    }
}