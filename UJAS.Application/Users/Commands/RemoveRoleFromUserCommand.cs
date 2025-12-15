using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class RemoveRoleFromUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Guid RemovedBy { get; set; }
        public string Reason { get; set; }

        public RemoveRoleFromUserCommand(Guid userId, Guid roleId, Guid removedBy)
        {
            UserId = userId;
            RoleId = roleId;
            RemovedBy = removedBy;
        }
    }
}
