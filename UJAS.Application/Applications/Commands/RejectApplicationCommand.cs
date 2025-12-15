using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class RejectApplicationCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public Guid RejectedById { get; set; }
        public string Reason { get; set; }
        public string Feedback { get; set; }

        public RejectApplicationCommand(Guid applicationId, Guid rejectedById)
        {
            ApplicationId = applicationId;
            RejectedById = rejectedById;
        }
    }
}