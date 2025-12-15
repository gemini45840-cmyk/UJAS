using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Profiles.DTOs
{
    public class ProfileUpdateDto
    {
        public Guid ProfileId { get; set; }

        // Basic Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProfilePictureUrl { get; set; }

        // Personal Information
        public PersonalInformationUpdateDto PersonalInformation { get; set; }

        // Employment Information
        public EmploymentInformationUpdateDto EmploymentInformation { get; set; }

        // Education History
        public List<EducationHistoryUpdateDto> EducationHistory { get; set; }

        // Work Experience
        public List<WorkExperienceUpdateDto> WorkExperience { get; set; }

        // Skills
        public SkillsUpdateDto Skills { get; set; }

        // Licenses & Certifications
        public List<LicenseCertificationUpdateDto> LicensesCertifications { get; set; }

        // Documents
        public DocumentsUpdateDto Documents { get; set; }

        // References
        public List<ReferenceUpdateDto> References { get; set; }

        // Emergency Contacts
        public List<EmergencyContactUpdateDto> EmergencyContacts { get; set; }

        // Settings
        public ProfileSettingsUpdateDto ProfileSettings { get; set; }

        // Communication Preferences
        public CommunicationPreferencesUpdateDto CommunicationPreferences { get; set; }
    }

    public class PersonalInformationUpdateDto
    {
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
        public AddressUpdateDto CurrentAddress { get; set; }
        public bool? IsMailingSameAsCurrent { get; set; }
        public AddressUpdateDto MailingAddress { get; set; }
        public int? YearsAtCurrentAddress { get; set; }
        public string GenderIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EthnicityRace { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorizationStatus { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string PortfolioUrl { get; set; }
    }

    public class EmploymentInformationUpdateDto
    {
        public bool? IsLegallyAuthorizedToWork { get; set; }
        public bool? RequiresVisaSponsorship { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }
        public bool? HasFelonyConviction { get; set; }
        public string FelonyExplanation { get; set; }
        public bool? WillingBackgroundCheck { get; set; }
        public bool? WillingDrugTest { get; set; }
        public string DesiredJobTitle { get; set; }
        public List<string> DesiredIndustries { get; set; }
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public string WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public decimal? DesiredSalaryFrom { get; set; }
        public decimal? DesiredSalaryTo { get; set; }
        public bool? WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public List<string> PreferredLocations { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public string NoticePeriodRequired { get; set; }
        public bool? IsCurrentlyEmployed { get; set; }
        public string CurrentEmployer { get; set; }
        public string JobSearchStatus { get; set; }
        public bool? WillingToTravel { get; set; }
        public int? TravelPercentage { get; set; }
        public bool? WillingToWorkRemote { get; set; }
        public int? RemoteWorkPreference { get; set; }
    }

    public class EducationHistoryUpdateDto
    {
        public Guid? Id { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionType { get; set; }
        public string Location { get; set; }
        public string DegreeCertificate { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public bool? IsCurrentlyAttending { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool? EducationVerificationConsent { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class WorkExperienceUpdateDto
    {
        public Guid? Id { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public string EmploymentType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentEmployer { get; set; }
        public string ReasonForLeaving { get; set; }
        public string JobResponsibilities { get; set; }
        public List<string> KeyAccomplishments { get; set; }
        public List<string> SkillsUtilized { get; set; }
        public string SupervisorName { get; set; }
        public bool? MayContactEmployer { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class SkillsUpdateDto
    {
        public List<SkillProficiencyUpdateDto> TechnicalSkills { get; set; }
        public List<ProgrammingLanguageUpdateDto> ProgrammingLanguages { get; set; }
        public List<string> OperatingSystems { get; set; }
        public List<string> Databases { get; set; }
        public List<string> Frameworks { get; set; }
        public List<string> Tools { get; set; }
        public List<LanguageProficiencyUpdateDto> Languages { get; set; }
        public List<string> SoftSkills { get; set; }
    }

    public class SkillProficiencyUpdateDto
    {
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
    }

    public class ProgrammingLanguageUpdateDto
    {
        public string Language { get; set; }
        public string ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
    }

    public class LanguageProficiencyUpdateDto
    {
        public string Language { get; set; }
        public string Proficiency { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanWrite { get; set; }
        public bool? CanSpeak { get; set; }
    }

    public class LicenseCertificationUpdateDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DocumentsUpdateDto
    {
        public string ResumeUrl { get; set; }
        public string ResumeFileName { get; set; }
        public string ResumeVisibility { get; set; }
        public List<Guid> ResumeVisibleToCompanies { get; set; }
        public string CoverLetterUrl { get; set; }
        public string CoverLetterText { get; set; }
        public List<SupportingDocumentUpdateDto> SupportingDocuments { get; set; }
    }

    public class SupportingDocumentUpdateDto
    {
        public Guid? Id { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ReferenceUpdateDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Relationship { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? PermissionToContact { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmergencyContactUpdateDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public bool? IsPrimaryContact { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ProfileSettingsUpdateDto
    {
        public string ProfileVisibility { get; set; }
        public List<Guid> VisibleToCompanies { get; set; }
        public bool? ShowContactInfoToCompanies { get; set; }
        public bool? ShowSalaryHistory { get; set; }
        public bool? AutoFillApplications { get; set; }
        public bool? SaveApplicationDrafts { get; set; }
        public bool? NotifyOnProfileViews { get; set; }
        public bool? Require2FA { get; set; }
    }

    public class CommunicationPreferencesUpdateDto
    {
        public bool? ReceiveApplicationUpdates { get; set; }
        public bool? ReceiveAssessmentInvitations { get; set; }
        public bool? ReceiveJobAlerts { get; set; }
        public bool? ReceiveSMSNotifications { get; set; }
        public bool? AllowMarketingEmails { get; set; }
        public List<string> NotificationMethods { get; set; }
    }
}