using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUserRolesQuery : IRequest<List<UserRoleDto>>
    {
        public Guid UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }

        public GetUserRolesQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
