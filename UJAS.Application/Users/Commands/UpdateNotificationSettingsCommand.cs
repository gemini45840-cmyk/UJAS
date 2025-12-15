using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class UpdateNotificationSettingsCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public bool? EmailEnabled { get; set; }
        public bool? PushEnabled { get; set; }
        public bool? SmsEnabled { get; set; }
        public bool? InAppEnabled { get; set; }
        public Dictionary<string, bool> CategoryPreferences { get; set; }
        public bool? QuietHoursEnabled { get; set; }
        public TimeSpan? QuietHoursStart { get; set; }
        public TimeSpan? QuietHoursEnd { get; set; }
        public List<string> QuietDays { get; set; }
        public bool? DailyDigest { get; set; }
        public bool? WeeklyDigest { get; set; }
        public TimeSpan? DigestTime { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateNotificationSettingsCommand(Guid userId, Guid updatedBy)
        {
            UserId = userId;
            UpdatedBy = updatedBy;
        }
    }
}
