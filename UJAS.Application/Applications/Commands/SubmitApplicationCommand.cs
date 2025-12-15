using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class SubmitApplicationCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public Guid ApplicantId { get; set; }
        public string Signature { get; set; }
        public string IpAddress { get; set; }

        public SubmitApplicationCommand(Guid applicationId, Guid applicantId)
        {
            ApplicationId = applicationId;
            ApplicantId = applicantId;
        }
    }
}