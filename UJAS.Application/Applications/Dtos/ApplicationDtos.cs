using UJAS.Application.Common.Services;
using UJAS.Core.Enums;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid LocationId { get; set; }
        public string Position { get; set; }
        public ApplicationStatusDto Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string ApplicantName { get; set; }
        public string CompanyName { get; set; }
        public string LocationName { get; set; }
        public bool HasAssessments { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        public string WithdrawalReason { get; set; }
        public string RejectionReason { get; set; }
        public ApplicationSourceDto Source { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignCode { get; set; }
    }
    public enum ApplicationStatusDto
    {
        Draft,
        Submitted,
        UnderReview,
        AssessmentPending,
        AssessmentCompleted,
        InterviewScheduled,
        InterviewCompleted,
        BackgroundCheck,
        OfferExtended,
        Accepted,
        Rejected,
        Withdrawn,
        OnHold
    }
    public enum ApplicationSourceDto
    {
        Widget,
        DirectLink,
        QRCode,
        API,
        Internal
    }
}
