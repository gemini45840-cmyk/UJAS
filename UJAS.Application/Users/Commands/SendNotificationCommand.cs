using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class SendNotificationCommand : IRequest<Guid>
    {
        public NotificationCreateDto Notification { get; set; }
        public Guid SentBy { get; set; }

        public SendNotificationCommand(NotificationCreateDto notification, Guid sentBy)
        {
            Notification = notification;
            SentBy = sentBy;
        }
    }
}