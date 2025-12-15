using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class DeleteProfileCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool PermanentDelete { get; set; } = false;
        public bool ArchiveData { get; set; } = true;
        public bool SendConfirmationEmail { get; set; } = true;

        public DeleteProfileCommand(Guid profileId, Guid deletedBy)
        {
            ProfileId = profileId;
            DeletedBy = deletedBy;
        }
    }
}