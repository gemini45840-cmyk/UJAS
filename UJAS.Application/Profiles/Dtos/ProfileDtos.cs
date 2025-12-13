using UJAS.Application.Common.DTOs;
using UJAS.Core.Enums;

namespace UJAS.Application.Profiles.Dtos
{
    public class ApplicantProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Personal Information
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PreferredContactMethod { get; set; }
        public string BestTimeToContact { get; set; }

        // Address
        public AddressDto CurrentAddress { get; set; }
        public bool SameAsMailingAddress { get; set; }
        public AddressDto MailingAddress { get; set; }
        public int? YearsAtCurrentAddress { get; set; }
        public string PreviousAddress { get; set; }

        // Demographic Information
        public string GenderIdentity { get; set; }
        public string GenderSelfDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Ethnicity { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorization { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public bool? WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public SalaryRangeDto DesiredSalaryRange { get; set; }
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
        public string ResumeUrl { get; set; }
        public DateTime? ResumeLastUpdated { get; set; }
        public string ResumeVisibility { get; set; }

        // Education
        public List<EducationHistoryDto> EducationHistories { get; set; } = new();
        public List<LicenseCertificationDto> LicensesCertifications { get; set; } = new();

        // Work Experience
        public List<WorkExperienceDto> WorkExperiences { get; set; } = new();
        public MilitaryServiceDto MilitaryService { get; set; }

        // Skills
        public List<SkillDto> Skills { get; set; } = new();

        // Documents
        public List<DocumentDto> Documents { get; set; } = new();

        // References
        public List<ReferenceDto> References { get; set; } = new();

        // Emergency Contacts
        public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();

        // Drivers License
        public DriversLicenseInfoDto DriversLicenseInfo { get; set; }

        // Criminal History
        public CriminalHistoryDto CriminalHistory { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ApplicationCount { get; set; }
    }

    public class UpdateProfileDto
    {
        // Personal Information
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PreferredContactMethod { get; set; }
        public string BestTimeToContact { get; set; }

        // Address
        public AddressDto CurrentAddress { get; set; }
        public bool SameAsMailingAddress { get; set; }
        public AddressDto MailingAddress { get; set; }
        public int? YearsAtCurrentAddress { get; set; }
        public string PreviousAddress { get; set; }

        // Demographic Information (Optional)
        public string GenderIdentity { get; set; }
        public string GenderSelfDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Ethnicity { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorization { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public bool? WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public SalaryRangeDto DesiredSalaryRange { get; set; }
        public bool? WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public int? NoticePeriodDays { get; set; }
        public string ReferredByEmployee { get; set; }
        public string HowDidYouHear { get; set; }
    }

    public class EducationHistoryDto
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionType { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string DegreeCertificate { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public DateTime? GraduationDate { get; set; }
        public DateTime? ExpectedGraduationDate { get; set; }
        public bool IsCurrentlyAttending { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool? EducationVerificationConsent { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class WorkExperienceDto
    {
        public int Id { get; set; }
        public string EmployerName { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerStateProvince { get; set; }
        public string EmployerCountry { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentEmployer { get; set; }
        public string ReasonForLeaving { get; set; }
        public decimal? StartingSalary { get; set; }
        public decimal? EndingSalary { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string SupervisorContact { get; set; }
        public bool? CanContactEmployer { get; set; }
        public string JobResponsibilities { get; set; }
        public string KeyAccomplishments { get; set; }
        public string SkillsUtilized { get; set; }
        public string EquipmentSoftwareUsed { get; set; }
        public int? NumberOfEmployeesSupervised { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class SkillDto
    {
        public int Id { get; set; }
        public string SkillType { get; set; }
        public string Name { get; set; }
        public string ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class DocumentDto
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}