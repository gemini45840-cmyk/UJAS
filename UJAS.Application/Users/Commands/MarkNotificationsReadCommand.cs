using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class MarkNotificationsReadCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public List<Guid> NotificationIds { get; set; } = new();
        public bool MarkAllRead { get; set; } = false;
        public DateTime? ReadBefore { get; set; }

        public MarkNotificationsReadCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
