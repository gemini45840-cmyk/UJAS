using UJAS.Core.Entities.User;

namespace UJAS.Core.Entities.System
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // Info, Success, Warning, Error
        public bool IsRead { get; set; } = false;
        public string ActionUrl { get; set; }
        public DateTime? ReadDate { get; set; }

        // Navigation properties
        public virtual tUser User { get; set; }
    }
}