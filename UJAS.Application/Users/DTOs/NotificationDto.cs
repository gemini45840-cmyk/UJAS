using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // Info, Success, Warning, Error, System
        public string Category { get; set; } // Application, Assessment, System, Security
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string ActionUrl { get; set; }
        public string ActionText { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
        public string Priority { get; set; } // Low, Medium, High, Critical
    }

    public class NotificationSettingsDto
    {
        public Guid UserId { get; set; }
        public bool EmailEnabled { get; set; } = true;
        public bool PushEnabled { get; set; } = true;
        public bool SmsEnabled { get; set; } = false;
        public bool InAppEnabled { get; set; } = true;

        // Category preferences
        public Dictionary<string, bool> CategoryPreferences { get; set; } = new()
        {
            ["Application"] = true,
            ["Assessment"] = true,
            ["System"] = true,
            ["Security"] = true
        };

        // Time restrictions
        public bool QuietHoursEnabled { get; set; } = false;
        public TimeSpan QuietHoursStart { get; set; } = new TimeSpan(22, 0, 0); // 10 PM
        public TimeSpan QuietHoursEnd { get; set; } = new TimeSpan(8, 0, 0); // 8 AM
        public List<string> QuietDays { get; set; } = new() { "Saturday", "Sunday" };

        // Digest preferences
        public bool DailyDigest { get; set; } = true;
        public bool WeeklyDigest { get; set; } = true;
        public TimeSpan DigestTime { get; set; } = new TimeSpan(9, 0, 0); // 9 AM
    }

    public class NotificationCreateDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } = "Info";
        public string Category { get; set; } = "System";
        public string ActionUrl { get; set; }
        public string ActionText { get; set; }
        public string Priority { get; set; } = "Medium";
        public DateTime? ExpiryDate { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }
}
