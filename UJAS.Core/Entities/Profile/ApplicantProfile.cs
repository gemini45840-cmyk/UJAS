using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.System;
using UJAS.Core.Enums;
using UJAS.Core.Shared;

namespace UJAS.Core.Entities.Profile
{
    public class ApplicantProfile : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Personal Information
        public Salutation Salutation { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string PreferredFirstName { get; set; }

        public ValueObjects.ContactInfo ContactInfo { get; set; }
        public ValueObjects.Address CurrentAddress { get; set; }
        public ValueObjects.Address PreviousAddress { get; set; }

        // Demographic Information (Voluntary)
        public GenderIdentity? GenderIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Ethnicity? Ethnicity { get; set; }
        public VeteranStatus? VeteranStatus { get; set; }
        public DisabilityStatus? DisabilityStatus { get; set; }
        public WorkAuthorization? WorkAuthorization { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public EmploymentType? DesiredEmploymentType { get; set; }
        public WorkSchedule? WorkSchedulePreference { get; set; }
        public ShiftAvailability? ShiftAvailability { get; set; }
        public bool? WillingToWorkOvertime { get; set; }
        public decimal? MinimumSalary { get; set; }
        public decimal? DesiredSalaryFrom { get; set; }
        public decimal? DesiredSalaryTo { get; set; }
        public bool WillingToRelocate { get; set; }
        public int? RelocationRadiusMiles { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public int? NoticePeriodDays { get; set; }
        public string ReferredByEmployee { get; set; }
        public ReferralSource? ReferralSource { get; set; }

        // Background Check Information
        public bool? LegallyAuthorizedToWork { get; set; }
        public bool? RequiresVisaSponsorship { get; set; }
        public bool? FelonyConviction { get; set; }
        public string FelonyExplanation { get; set; }
        public bool? WillingBackgroundCheck { get; set; }
        public bool? WillingDrugTest { get; set; }

        // Driver's License
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseState { get; set; }
        public DateTime? DriversLicenseExpiration { get; set; }
        public string DriversLicenseClass { get; set; }

        // Criminal History
        public bool? CriminalConviction { get; set; }
        public string CriminalConvictionDetails { get; set; }
        public bool? PendingCharges { get; set; }
        public string PendingChargesDetails { get; set; }

        // Navigation Properties
        public virtual ICollection<Education> EducationHistory { get; set; } = new List<Education>();
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
    }
}