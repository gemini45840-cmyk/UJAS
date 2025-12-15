using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUserActivityQuery : IRequest<PaginatedList<UserAuditLogDto>>
    {
        public Guid UserId { get; set; }
        public List<string> Actions { get; set; } = new();
        public List<string> EntityTypes { get; set; } = new();
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "Timestamp";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public GetUserActivityQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
