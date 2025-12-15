using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetNotificationsQuery : IRequest<PaginatedList<NotificationDto>>
    {
        public Guid UserId { get; set; }
        public bool UnreadOnly { get; set; } = false;
        public List<string> Types { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public DateTime? SinceDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public string SortBy { get; set; } = "CreatedDate";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public GetNotificationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
