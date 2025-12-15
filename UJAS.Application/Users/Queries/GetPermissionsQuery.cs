using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetPermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string Module { get; set; }
        public string Category { get; set; }
        public bool? IsSystemPermission { get; set; }
        public Guid? CompanyId { get; set; }
        public string UserType { get; set; }
    }
}
