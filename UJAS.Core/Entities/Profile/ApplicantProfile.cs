using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UJAS.Core.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.Profile
{
    public class ApplicantProfile : BaseAuditableEntity
    {
        public int UserId { get; set; }

        // Personal Information
        public Salutation? Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public ContactMethod? PreferredContactMethod { get; set; }
        public ContactTime? BestTimeToContact { get; set; }

        // Address
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public AddressType? AddressType { get; set; }
        public int? YearsAtCurrentAddress { get; set; }
        public string PreviousAddress { get; set; }
        public bool SameAsMailingAddress { get; set; } = true;

        // Demographic Information
        public GenderIdentity? GenderIdentity { get; set; }
        public string GenderSelfDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public VeteranStatus? VeteranStatus { get; set; }
        public DisabilityStatus? DisabilityStatus { get; set; }
        public WorkAuthorizationType? WorkAuthorization { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public EmploymentType? EmploymentTypeDesired { get; set; }
        public WorkSchedule? WorkSchedulePreference { get; set; }
        public ShiftAvailability? ShiftAvailability { get; set; }
        public bool? WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public decimal? DesiredSalaryFrom { get; set; }
        public decimal? DesiredSalaryTo { get; set; }
        public bool? WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public int? NoticePeriodDays { get; set; }
        public string ReferredByEmployee { get; set; }
        public string HowDidYouHear { get; set; }

        // Background Check
        public bool? LegallyAuthorizedToWork { get; set; }
        public bool? RequiresVisaSponsorship { get; set; }
        public bool? FelonyConviction { get; set; }
        public string FelonyExplanation { get; set; }
        public bool? WillingBackgroundCheck { get; set; }
        public bool? WillingDrugTest { get; set; }

        // Resume
        public string ResumeFilePath { get; set; }
        public DateTime? ResumeLastUpdated { get; set; }
        public VisibilitySetting? ResumeVisibility { get; set; }

        // Photo
        public string PhotoUrl { get; set; }
        public bool? PhotoUsageConsent { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<EducationHistory> EducationHistories { get; set; } = new List<EducationHistory>();
        public virtual ICollection<LicenseCertification> LicensesCertifications { get; set; } = new List<LicenseCertification>();
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual MilitaryService MilitaryService { get; set; }
        public virtual DriversLicenseInfo DriversLicenseInfo { get; set; }
        public virtual CriminalHistory CriminalHistory { get; set; }
    }
}