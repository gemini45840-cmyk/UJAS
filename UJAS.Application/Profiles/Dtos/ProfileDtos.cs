using UJAS.Core.Enums;

namespace UJAS.Application.Profiles.Dtos
{
    public class ApplicantProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }

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
        public bool SameAsMailingAddress { get; set; }

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

        // System
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsComplete { get; set; }
        public int CompletionPercentage { get; set; }

        // Navigation properties
        public List<EducationHistoryDto> EducationHistories { get; set; } = new();
        public List<WorkExperienceDto> WorkExperiences { get; set; } = new();
        public List<SkillDto> Skills { get; set; } = new();
        public List<LicenseCertificationDto> LicensesCertifications { get; set; } = new();
        public List<DocumentDto> Documents { get; set; } = new();
        public List<ReferenceDto> References { get; set; } = new();
        public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();
        public MilitaryServiceDto MilitaryService { get; set; }
        public DriversLicenseInfoDto DriversLicenseInfo { get; set; }
        public CriminalHistoryDto CriminalHistory { get; set; }

        // Statistics
        public ProfileStatisticsDto Statistics { get; set; }
    }

    public class UpdateApplicantProfileDto
    {
        // Personal Information
        public Salutation? Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
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
        public bool SameAsMailingAddress { get; set; }

        // Demographic Information (voluntary)
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
        public VisibilitySetting? ResumeVisibility { get; set; }

        // Photo
        public string PhotoUrl { get; set; }
        public bool? PhotoUsageConsent { get; set; }
    }

    public class EducationHistoryDto
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public EducationLevel InstitutionType { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public DegreeType? DegreeCertificate { get; set; }
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

    public class CreateEducationHistoryDto
    {
        public string InstitutionName { get; set; }
        public EducationLevel InstitutionType { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public DegreeType? DegreeCertificate { get; set; }
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
        public string Duration { get; set; }
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
        public SkillType SkillType { get; set; }
        public string Name { get; set; }
        public ProficiencyLevel? ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
        public DateTime? LastUsedDate { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class ProfileStatisticsDto
    {
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int AcceptedApplications { get; set; }
        public int RejectedApplications { get; set; }
        public DateTime? LastApplicationDate { get; set; }
        public int CompaniesAppliedTo { get; set; }
        public int PositionsAppliedFor { get; set; }
        public double AverageApplicationCompletionTime { get; set; } // in minutes
    }
}