using UJAS.Core.Entities.User;

namespace UJAS.Core.Entities.Application
{
    public class ApplicationComment : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public bool IsInternal { get; set; } = false;
        public bool VisibleToApplicant { get; set; } = false;

        // Navigation properties
        public virtual tApplication Application { get; set; }
        public virtual tUser User { get; set; }
    }
}
