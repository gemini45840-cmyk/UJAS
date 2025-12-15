using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetRolesQuery : IRequest<PaginatedList<RoleDto>>
    {
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public List<string> RoleTypes { get; set; } = new();
        public string SearchTerm { get; set; }
        public bool? IsSystemRole { get; set; }
        public bool? IsDefault { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
