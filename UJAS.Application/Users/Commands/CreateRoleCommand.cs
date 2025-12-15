using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public RoleCreateDto Role { get; set; }
        public Guid CreatedBy { get; set; }

        public CreateRoleCommand(RoleCreateDto role, Guid createdBy)
        {
            Role = role;
            CreatedBy = createdBy;
        }
    }
}