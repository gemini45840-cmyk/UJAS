using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid RoleId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public Guid? TransferToRoleId { get; set; }

        public DeleteRoleCommand(Guid roleId, Guid deletedBy)
        {
            RoleId = roleId;
            DeletedBy = deletedBy;
        }
    }
}
