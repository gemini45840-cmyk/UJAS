using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetNotificationSettingsQuery : IRequest<NotificationSettingsDto>
    {
        public Guid UserId { get; set; }

        public GetNotificationSettingsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
