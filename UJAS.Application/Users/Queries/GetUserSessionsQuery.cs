using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUserSessionsQuery : IRequest<List<SessionDto>>
    {
        public Guid UserId { get; set; }
        public bool ActiveOnly { get; set; } = false;
        public DateTime? SinceDate { get; set; }

        public GetUserSessionsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}