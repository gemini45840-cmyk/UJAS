using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public Guid LocationId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool TransferApplications { get; set; } = true;
        public Guid? TransferToLocationId { get; set; }

        public DeleteLocationCommand(Guid locationId, Guid deletedBy)
        {
            LocationId = locationId;
            DeletedBy = deletedBy;
        }
    }
}