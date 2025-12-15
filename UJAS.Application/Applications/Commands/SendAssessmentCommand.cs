using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class SendAssessmentCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public List<Guid> AssessmentIds { get; set; }
        public Guid SentById { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Instructions { get; set; }

        public SendAssessmentCommand(Guid applicationId, List<Guid> assessmentIds)
        {
            ApplicationId = applicationId;
            AssessmentIds = assessmentIds;
        }
    }
}
