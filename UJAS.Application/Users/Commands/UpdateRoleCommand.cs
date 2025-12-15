using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public RoleUpdateDto Role { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateRoleCommand(RoleUpdateDto role, Guid updatedBy)
        {
            Role = role;
            UpdatedBy = updatedBy;
        }
    }
}
