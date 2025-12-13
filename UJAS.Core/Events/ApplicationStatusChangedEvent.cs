using UJAS.Core.Enums;
using UJAS.Core.Interfaces;

namespace UJAS.Core.Events
{
    public class ApplicationStatusChangedEvent : IDomainEvent
    {
        public int ApplicationId { get; }
        public ApplicationStatus PreviousStatus { get; }
        public ApplicationStatus NewStatus { get; }
        public string ChangedBy { get; }
        public DateTime OccurredOn { get; }

        public ApplicationStatusChangedEvent(int applicationId,
            ApplicationStatus previousStatus, ApplicationStatus newStatus,
            string changedBy)
        {
            ApplicationId = applicationId;
            PreviousStatus = previousStatus;
            NewStatus = newStatus;
            ChangedBy = changedBy;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
