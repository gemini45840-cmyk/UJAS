using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Application
{
    public class ApplicationStatusHistory : BaseEntity
    {
        public int ApplicationId { get; set; }
        public ApplicationStatus PreviousStatus { get; set; }
        public ApplicationStatus NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public virtual tApplication Application { get; set; }
    }
}