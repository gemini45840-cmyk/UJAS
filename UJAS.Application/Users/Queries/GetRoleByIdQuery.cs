using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDetailDto>
    {
        public Guid RoleId { get; set; }
        public bool IncludeUsers { get; set; } = true;
        public bool IncludePermissions { get; set; } = true;

        public GetRoleByIdQuery(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}
