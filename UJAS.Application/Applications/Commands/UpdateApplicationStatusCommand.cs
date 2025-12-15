using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;

namespace UJAS.Application.Applications.Commands
{
    public class UpdateApplicationStatusCommand : IRequest<bool>
    {
        public Guid ApplicationId { get; set; }
        public ApplicationStatusDto NewStatus { get; set; }
        public Guid ChangedById { get; set; }
        public string ChangedByName { get; set; }
        public string ChangeReason { get; set; }
        public string Notes { get; set; }

        public UpdateApplicationStatusCommand(Guid applicationId, ApplicationStatusDto newStatus)
        {
            ApplicationId = applicationId;
            NewStatus = newStatus;
        }
    }
}