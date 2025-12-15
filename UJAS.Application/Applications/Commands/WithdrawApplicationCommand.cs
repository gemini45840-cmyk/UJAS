using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class WithdrawApplicationCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public Guid ApplicantId { get; set; }
        public string Reason { get; set; }

        public WithdrawApplicationCommand(Guid applicationId, Guid applicantId)
        {
            ApplicationId = applicationId;
            ApplicantId = applicantId;
        }
    }
}
