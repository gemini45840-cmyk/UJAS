using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.DTOs
{
    public class ProfileDetailDto : ProfileDto
    {
        // Personal Information
        public PersonalInformationDto PersonalInformation { get; set; }

        // Employment Information
        public EmploymentInformationDto EmploymentInformation { get; set; }

        // Education History
        public List<EducationHistoryDto> EducationHistory { get; set; } = new();

        // Work Experience
        public List<WorkExperienceDto> WorkExperience { get; set; } = new();

        // Military Service
        public MilitaryServiceDto MilitaryService { get; set; }

        // Skills
        public SkillsDto Skills { get; set; }

        // Licenses & Certifications
        public List<LicenseCertificationDto> LicensesCertifications { get; set; } = new();

        // Documents
        public DocumentsDto Documents { get; set; }

        // Background Information
        public BackgroundInformationDto BackgroundInformation { get; set; }

        // References
        public List<ReferenceDto> References { get; set; } = new();

        // Emergency Contacts
        public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();

        // Communication Preferences
        public CommunicationPreferencesDto CommunicationPreferences { get; set; }

        // Application History
        public List<ApplicationHistoryDto> ApplicationHistory { get; set; } = new();

        // Assessment History
        public List<AssessmentHistoryDto> AssessmentHistory { get; set; } = new();

        // Saved Jobs
        public List<SavedJobDto> SavedJobs { get; set; } = new();

        // Profile Settings
        public ProfileSettingsDto ProfileSettings { get; set; }

        // Audit Log
        public List<ProfileAuditLogDto> RecentAuditLogs { get; set; } = new();
    }

    public class PersonalInformationDto
    {
        // Basic Information
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string Suffix { get; set; }

        // Contact Information
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PreferredContactMethod { get; set; }
        public string BestTimeToContact { get; set; }

        // Address
        public AddressDto CurrentAddress { get; set; }
        public bool IsMailingSameAsCurrent { get; set; } = true;
        public AddressDto MailingAddress { get; set; }
        public int YearsAtCurrentAddress { get; set; }
        public AddressDto PreviousAddress { get; set; }

        // Demographic Information (Voluntary)
        public string GenderIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EthnicityRace { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorizationStatus { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public int? DependentsCount { get; set; }

        // Social Media
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string PortfolioUrl { get; set; }
        public string PersonalWebsite { get; set; }

        // Verification
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? EmailVerifiedDate { get; set; }
        public DateTime? PhoneVerifiedDate { get; set; }
    }

    public class EmploymentInformationDto
    {
        // Work Authorization
        public bool IsLegallyAuthorizedToWork { get; set; }
        public bool RequiresVisaSponsorship { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }
        public string CitizenshipStatus { get; set; }

        // Background Information
        public bool HasFelonyConviction { get; set; }
        public string FelonyExplanation { get; set; }
        public bool WillingBackgroundCheck { get; set; }
        public bool WillingDrugTest { get; set; }
        public bool HasNonCompeteAgreement { get; set; }
        public string NonCompeteDetails { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public List<string> DesiredIndustries { get; set; } = new();
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public string WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public decimal? DesiredSalaryFrom { get; set; }
        public decimal? DesiredSalaryTo { get; set; }
        public string SalaryCurrency { get; set; } = "USD";
        public bool WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public List<string> PreferredLocations { get; set; } = new();
        public DateTime? AvailableStartDate { get; set; }
        public string NoticePeriodRequired { get; set; }
        public bool IsCurrentlyEmployed { get; set; }
        public string CurrentEmployer { get; set; }
        public bool CanContactCurrentEmployer { get; set; }

        // Job Search Status
        public string JobSearchStatus { get; set; } // ActivelyLooking, OpenToOpportunities, NotLooking
        public int? DesiredHoursPerWeek { get; set; }
        public bool WillingToTravel { get; set; }
        public int? TravelPercentage { get; set; }
        public bool WillingToWorkRemote { get; set; }
        public int? RemoteWorkPreference { get; set; } // 0-100 percentage

        // Referral
        public string ReferralSource { get; set; }
        public string HowDidYouHear { get; set; }
        public string ReferredByEmployee { get; set; }
    }

    public class EducationHistoryDto
    {
        public Guid Id { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionType { get; set; } // HighSchool, College, University, Technical, Trade, Online
        public string Location { get; set; }
        public string Country { get; set; }
        public string DegreeCertificate { get; set; }
        public string FieldOfStudy { get; set; }
        public string MinorConcentration { get; set; }
        public decimal? GPA { get; set; }
        public decimal? GPAScale { get; set; } = 4.0m;
        public DateTime? StartDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public DateTime? ExpectedGraduationDate { get; set; }
        public bool IsCurrentlyAttending { get; set; }
        public bool IsGraduated { get; set; }
        public string HonorsAwards { get; set; }
        public string RelevantCoursework { get; set; }
        public bool EducationVerificationConsent { get; set; }
        public string TranscriptUrl { get; set; }
        public string DiplomaUrl { get; set; }
        public bool IsPrimaryEducation { get; set; }
    }

    public class WorkExperienceDto
    {
        public Guid Id { get; set; }
        public string EmployerName { get; set; }
        public string EmployerIndustry { get; set; }
        public AddressDto EmployerAddress { get; set; }
        public string JobTitle { get; set; }
        public string EmploymentType { get; set; } // FullTime, PartTime, Contract, Internship, Seasonal
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentEmployer { get; set; }
        public string ReasonForLeaving { get; set; }
        public decimal? StartingSalary { get; set; }
        public decimal? EndingSalary { get; set; }
        public string SalaryCurrency { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorTitle { get; set; }
        public string SupervisorContact { get; set; }
        public bool MayContactEmployer { get; set; }
        public string JobResponsibilities { get; set; }
        public List<string> KeyAccomplishments { get; set; } = new();
        public List<string> SkillsUtilized { get; set; } = new();
        public List<string> ToolsTechnologies { get; set; } = new();
        public int? NumberOfEmployeesSupervised { get; set; }
        public string TeamSize { get; set; }
        public string ProjectsWorkedOn { get; set; }
        public bool IsConfidential { get; set; }
        public bool ShowSalary { get; set; }
        public int Order { get; set; }
    }

    public class MilitaryServiceDto
    {
        public bool HasMilitaryService { get; set; }
        public string BranchOfService { get; set; }
        public string RankAtDischarge { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public string TypeOfDischarge { get; set; }
        public string MilitaryOccupationalSpecialty { get; set; }
        public string SecurityClearanceLevel { get; set; }
        public DateTime? SecurityClearanceExpiration { get; set; }
        public string RelevantSkillsTraining { get; set; }
        public string DD214Url { get; set; }
        public bool IsVeteran { get; set; }
        public string VeteranStatus { get; set; }
        public List<string> MilitaryAwards { get; set; } = new();
    }

    public class SkillsDto
    {
        // Technical Skills
        public List<SkillProficiencyDto> TechnicalSkills { get; set; } = new();
        public List<ProgrammingLanguageDto> ProgrammingLanguages { get; set; } = new();
        public List<string> OperatingSystems { get; set; } = new();
        public List<string> Databases { get; set; } = new();
        public List<string> Frameworks { get; set; } = new();
        public List<string> Tools { get; set; } = new();
        public List<string> CloudPlatforms { get; set; } = new();
        public List<string> DevOpsTools { get; set; } = new();

        // Professional Skills
        public List<LanguageProficiencyDto> Languages { get; set; } = new();
        public List<string> CommunicationSkills { get; set; } = new();
        public List<string> LeadershipSkills { get; set; } = new();
        public List<string> ProjectManagementSkills { get; set; } = new();
        public List<string> CustomerServiceSkills { get; set; } = new();
        public List<string> SalesMarketingSkills { get; set; } = new();
        public List<string> TeachingTrainingSkills { get; set; } = new();
        public List<string> ResearchSkills { get; set; } = new();
        public List<string> AnalyticalSkills { get; set; } = new();

        // Industry-Specific Skills
        public List<string> HealthcareSkills { get; set; } = new();
        public List<string> EngineeringSkills { get; set; } = new();
        public List<string> FinanceSkills { get; set; } = new();
        public List<string> LegalSkills { get; set; } = new();
        public List<string> CreativeSkills { get; set; } = new();
        public List<string> TradeSkills { get; set; } = new();

        // Soft Skills
        public List<string> SoftSkills { get; set; } = new();

        // Skill Assessments
        public List<SkillAssessmentDto> SkillAssessments { get; set; } = new();
    }

    public class SkillProficiencyDto
    {
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; } // Beginner, Intermediate, Advanced, Expert
        public int? YearsOfExperience { get; set; }
        public DateTime? LastUsed { get; set; }
        public bool IsCertified { get; set; }
        public string CertificationName { get; set; }
    }

    public class ProgrammingLanguageDto
    {
        public string Language { get; set; }
        public string ProficiencyLevel { get; set; }
        public int? YearsOfExperience { get; set; }
        public DateTime? LastUsed { get; set; }
        public List<string> Frameworks { get; set; } = new();
    }

    public class LanguageProficiencyDto
    {
        public string Language { get; set; }
        public string Proficiency { get; set; } // Native, Fluent, Conversational, Basic
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanSpeak { get; set; }
        public bool CanInterpret { get; set; }
        public bool IsCertified { get; set; }
        public string CertificationLevel { get; set; }
    }

    public class SkillAssessmentDto
    {
        public string AssessmentName { get; set; }
        public string Provider { get; set; }
        public DateTime? TakenDate { get; set; }
        public decimal? Score { get; set; }
        public string Result { get; set; }
        public string CertificateUrl { get; set; }
        public bool IsVerified { get; set; }
    }

    public class LicenseCertificationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public string LicenseNumber { get; set; }
        public string StateCountryOfIssue { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public bool IsExpired { get; set; }
        public bool RequiresRenewal { get; set; }
        public string FileUrl { get; set; }
        public string VerificationStatus { get; set; } // NotVerified, Pending, Verified, Expired
        public DateTime? VerifiedDate { get; set; }
        public bool IsPrimaryCertification { get; set; }
    }

    public class DocumentsDto
    {
        // Resume/CV
        public string ResumeUrl { get; set; }
        public string ResumeFileName { get; set; }
        public string ResumeFileType { get; set; }
        public long? ResumeFileSize { get; set; }
        public DateTime? ResumeLastUpdated { get; set; }
        public string ResumeVisibility { get; set; } // Public, Private, SelectedCompanies
        public List<Guid> ResumeVisibleToCompanies { get; set; } = new();

        // Cover Letter
        public string CoverLetterUrl { get; set; }
        public string CoverLetterFileName { get; set; }
        public string CoverLetterText { get; set; }
        public bool UseCustomCoverLetter { get; set; }

        // Portfolio
        public string PortfolioUrl { get; set; }
        public string PortfolioDescription { get; set; }

        // Supporting Documents
        public List<SupportingDocumentDto> SupportingDocuments { get; set; } = new();

        // Photo
        public string PhotoUrl { get; set; }
        public bool PhotoUsageConsent { get; set; }

        // Document Storage
        public long TotalStorageUsed { get; set; }
        public long MaxStorageAllowed { get; set; } = 100 * 1024 * 1024; // 100MB default
        public decimal StoragePercentageUsed { get; set; }
    }

    public class SupportingDocumentDto
    {
        public Guid Id { get; set; }
        public string DocumentType { get; set; } // Transcript, Diploma, Certificate, ReferenceLetter, WorkSample, Other
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string Visibility { get; set; } // Public, Private
    }

    public class BackgroundInformationDto
    {
        // Driver's License
        public bool HasDriversLicense { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseState { get; set; }
        public DateTime? DriversLicenseExpiration { get; set; }
        public string LicenseClass { get; set; }
        public string Endorsements { get; set; }
        public string Restrictions { get; set; }
        public string DriversLicenseUrl { get; set; }
        public bool DrivingRecordConsent { get; set; }

        // Criminal History
        public bool HasCriminalConviction { get; set; }
        public string CriminalConvictionDetails { get; set; }
        public bool HasPendingCharges { get; set; }
        public string PendingChargesDetails { get; set; }
        public bool BackgroundCheckConsent { get; set; }

        // Credit History
        public bool CreditCheckConsent { get; set; }

        // Social Security
        public string SocialSecurityNumber { get; set; } // Encrypted
        public bool SsnVerificationConsent { get; set; }
    }

    public class ReferenceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Relationship { get; set; } // Supervisor, Colleague, Professor, Client
        public int YearsKnown { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BestTimeToContact { get; set; }
        public bool PermissionToContact { get; set; }
        public string ReferenceType { get; set; } // Professional, Personal, Academic
        public bool IsPrimaryReference { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string Notes { get; set; }
    }

    public class EmergencyContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; } // Spouse, Parent, Sibling, Friend, Other
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public bool IsPrimaryContact { get; set; }
        public string Notes { get; set; }
    }

    public class CommunicationPreferencesDto
    {
        public bool ReceiveApplicationUpdates { get; set; } = true;
        public bool ReceiveAssessmentInvitations { get; set; } = true;
        public bool ReceiveInterviewInvitations { get; set; } = true;
        public bool ReceiveJobAlerts { get; set; } = true;
        public bool ReceiveNewsletters { get; set; } = false;
        public bool ReceiveSMSNotifications { get; set; } = false;
        public string PreferredLanguage { get; set; } = "en";
        public string TimeZone { get; set; }
        public List<string> NotificationMethods { get; set; } = new() { "Email" };
        public bool AllowMarketingEmails { get; set; } = false;
        public bool AllowThirdPartySharing { get; set; } = false;
        public bool AutoDeleteProfileAfterInactivity { get; set; } = true;
        public int InactivityMonthsBeforeDeletion { get; set; } = 24;
    }

    public class ApplicationHistoryDto
    {
        public Guid ApplicationId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public bool HasAssessment { get; set; }
        public bool HasInterview { get; set; }
        public bool HasOffer { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanReapply { get; set; }
        public DateTime? ReapplyDate { get; set; }
    }

    public class AssessmentHistoryDto
    {
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? InvitationDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Status { get; set; }
        public decimal? Score { get; set; }
        public bool Passed { get; set; }
        public bool IsProctored { get; set; }
        public bool HasResults { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class SavedJobDto
    {
        public Guid JobId { get; set; }
        public string Position { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime SavedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool HasApplied { get; set; }
        public DateTime? AppliedDate { get; set; }
        public string ApplicationStatus { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProfileSettingsDto
    {
        // Profile Visibility
        public string ProfileVisibility { get; set; } = "Private"; // Public, Private, SelectedCompanies
        public List<Guid> VisibleToCompanies { get; set; } = new();
        public bool ShowContactInfoToCompanies { get; set; } = true;
        public bool ShowSalaryHistory { get; set; } = false;
        public bool ShowEducationDetails { get; set; } = true;
        public bool ShowWorkExperience { get; set; } = true;
        public bool ShowSkills { get; set; } = true;
        public bool ShowAssessments { get; set; } = true;

        // Application Preferences
        public bool AutoFillApplications { get; set; } = true;
        public bool SaveApplicationDrafts { get; set; } = true;
        public bool AutoSaveProfileChanges { get; set; } = true;
        public int AutoSaveIntervalSeconds { get; set; } = 30;
        public bool NotifyOnProfileViews { get; set; } = false;
        public bool NotifyOnProfileUpdates { get; set; } = true;

        // Security
        public bool Require2FA { get; set; } = false;
        public bool RequirePasswordChange { get; set; } = false;
        public DateTime? PasswordLastChanged { get; set; }
        public List<LoginHistoryDto> RecentLogins { get; set; } = new();
        public List<DeviceDto> TrustedDevices { get; set; } = new();

        // Data Management
        public bool AllowDataExport { get; set; } = true;
        public DateTime? LastDataExportDate { get; set; }
        public bool AutoArchiveOldApplications { get; set; } = true;
        public int ArchiveAfterMonths { get; set; } = 12;
    }

    public class LoginHistoryDto
    {
        public DateTime LoginDate { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public bool Successful { get; set; }
        public string FailureReason { get; set; }
    }

    public class DeviceDto
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string OperatingSystem { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
        public bool IsTrusted { get; set; }
        public string Location { get; set; }
    }

    public class ProfileAuditLogDto
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string Section { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public Guid ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public string IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
