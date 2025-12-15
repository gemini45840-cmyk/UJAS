using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<PaginatedList<UserDto>>
    {
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public List<string> UserTypes { get; set; } = new();
        public List<string> Statuses { get; set; } = new();
        public string SearchTerm { get; set; }
        public bool? EmailVerified { get; set; }
        public bool? PhoneVerified { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public DateTime? LastLoginAfter { get; set; }
        public DateTime? LastLoginBefore { get; set; }
        public string SortBy { get; set; } = "LastName";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // Permissions filter
        public Guid? RequestedBy { get; set; }
        public string RequestedByRole { get; set; }
    }
}
