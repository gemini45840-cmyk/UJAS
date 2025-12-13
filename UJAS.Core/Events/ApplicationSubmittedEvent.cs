using UJAS.Core.Interfaces;

namespace UJAS.Core.Events
{
    public class ApplicationSubmittedEvent : IDomainEvent
    {
        public int ApplicationId { get; }
        public int ApplicantId { get; }
        public int CompanyId { get; }
        public string ApplicationNumber { get; }
        public DateTime OccurredOn { get; }

        public ApplicationSubmittedEvent(int applicationId, int applicantId,
            int companyId, string applicationNumber)
        {
            ApplicationId = applicationId;
            ApplicantId = applicantId;
            CompanyId = companyId;
            ApplicationNumber = applicationNumber;
            OccurredOn = DateTime.UtcNow;
        }
    }
}