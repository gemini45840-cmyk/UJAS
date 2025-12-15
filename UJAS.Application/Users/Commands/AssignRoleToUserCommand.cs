using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class AssignRoleToUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Guid AssignedBy { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }

        public AssignRoleToUserCommand(Guid userId, Guid roleId, Guid assignedBy)
        {
            UserId = userId;
            RoleId = roleId;
            AssignedBy = assignedBy;
        }
    }
}
