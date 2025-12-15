using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool PermanentDelete { get; set; } = false;
        public bool ArchiveData { get; set; } = true;
        public bool TransferOwnership { get; set; } = true;
        public Guid? TransferToUserId { get; set; }

        public DeleteUserCommand(Guid userId, Guid deletedBy)
        {
            UserId = userId;
            DeletedBy = deletedBy;
        }
    }
}
