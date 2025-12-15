using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class DeleteApplicationCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public Guid DeletedById { get; set; }
        public string DeletedByRole { get; set; }
        public string Reason { get; set; }

        public DeleteApplicationCommand(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
