namespace UJAS.Core.Enums
{
    public enum UserRole
    {
        SystemAdministrator = 1,
        CompanyAdministrator = 2,
        RegionalManager = 3,
        Manager = 4,
        Applicant = 5
    }
}

namespace UJAS.Core.Enums
{
    public enum ApplicationStatus
    {
        Draft = 1,
        Submitted = 2,
        UnderReview = 3,
        Shortlisted = 4,
        InterviewScheduled = 5,
        AssessmentRequired = 6,
        BackgroundCheck = 7,
        Accepted = 8,
        Rejected = 9,
        Withdrawn = 10,
        Archived = 11
    }
}

namespace UJAS.Core.Enums
{
    public enum Salutation
    {
        Mr = 1,
        Mrs = 2,
        Ms = 3,
        Mx = 4,
        Dr = 5,
        Prof = 6,
        Other = 7
    }
}

namespace UJAS.Core.Enums
{
    public enum ContactMethod
    {
        Email = 1,
        Phone = 2,
        Text = 3
    }
}

namespace UJAS.Core.Enums
{
    public enum ContactTime
    {
        Morning = 1,
        Afternoon = 2,
        Evening = 3,
        Any = 4
    }
}

namespace UJAS.Core.Enums
{
    public enum AddressType
    {
        Home = 1,
        Temporary = 2,
        Permanent = 3
    }
}

namespace UJAS.Core.Enums
{
    public enum GenderIdentity
    {
        Male = 1,
        Female = 2,
        NonBinary = 3,
        PreferNotToSay = 4,
        PreferToSelfDescribe = 5
    }
}

namespace UJAS.Core.Enums
{
    public enum Ethnicity
    {
        White = 1,
        BlackOrAfricanAmerican = 2,
        HispanicOrLatino = 3,
        Asian = 4,
        NativeAmerican = 5,
        PacificIslander = 6,
        TwoOrMoreRaces = 7,
        PreferNotToAnswer = 8
    }
}

namespace UJAS.Core.Enums
{
    public enum VeteranStatus
    {
        ProtectedVeteran = 1,
        DisabledVeteran = 2,
        RecentlySeparatedVeteran = 3,
        ActiveDutyWartime = 4,
        OtherProtectedVeteran = 5,
        NonVeteran = 6,
        PreferNotToSay = 7
    }
}

namespace UJAS.Core.Enums
{
    public enum DisabilityStatus
    {
        Yes = 1,
        No = 2,
        PreferNotToSay = 3
    }
}

namespace UJAS.Core.Enums
{
    public enum WorkAuthorizationType
    {
        USCitizen = 1,
        PermanentResident = 2,
        H1BVisa = 3,
        L1Visa = 4,
        F1Visa = 5,
        J1Visa = 6,
        TNVisa = 7,
        RequiresSponsorship = 8,
        OtherVisa = 9
    }
}

namespace UJAS.Core.Enums
{
    public enum EmploymentType
    {
        FullTime = 1,
        PartTime = 2,
        Contract = 3,
        Temporary = 4,
        Internship = 5,
        Seasonal = 6
    }
}

namespace UJAS.Core.Enums
{
    public enum WorkSchedule
    {
        Days = 1,
        Evenings = 2,
        Nights = 3,
        Weekends = 4,
        Rotating = 5,
        Flexible = 6
    }
}

namespace UJAS.Core.Enums
{
    public enum ShiftAvailability
    {
        First = 1,
        Second = 2,
        Third = 3,
        Any = 4
    }
}

namespace UJAS.Core.Enums
{
    public enum EducationLevel
    {
        HighSchool = 1,
        CollegeUniversity = 2,
        TechnicalSchool = 3,
        TradeSchool = 4,
        Online = 5,
        Other = 6
    }
}

namespace UJAS.Core.Enums
{
    public enum DegreeType
    {
        Diploma = 1,
        Associate = 2,
        Bachelor = 3,
        Master = 4,
        PhD = 5,
        Certificate = 6,
        Other = 7
    }
}

namespace UJAS.Core.Enums
{
    public enum ProficiencyLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Expert = 4,
        Native = 5,
        Fluent = 6,
        Conversational = 7,
        Basic = 8
    }
}

namespace UJAS.Core.Enums
{
    public enum SkillType
    {
        Technical = 1,
        Professional = 2,
        Soft = 3,
        Language = 4
    }
}

namespace UJAS.Core.Enums
{
    public enum DocumentType
    {
        Resume = 1,
        CoverLetter = 2,
        Portfolio = 3,
        Certificate = 4,
        License = 5,
        Transcript = 6,
        RecommendationLetter = 7,
        WritingSample = 8,
        DesignSample = 9,
        CodeSample = 10,
        WorkSample = 11,
        PresentationSample = 12,
        Headshot = 13,
        DriversLicense = 14,
        DD214 = 15,
        Other = 16
    }
}

namespace UJAS.Core.Enums
{
    public enum VisibilitySetting
    {
        VisibleToAll = 1,
        Hidden = 2,
        SpecificCompaniesOnly = 3
    }
}

namespace UJAS.Core.Enums
{
    public enum FieldType
    {
        Text = 1,
        TextArea = 2,
        Number = 3,
        Date = 4,
        Dropdown = 5,
        MultiSelect = 6,
        Radio = 7,
        FileUpload = 8,
        Email = 9,
        Phone = 10,
        Url = 11,
        Signature = 12,
        Rating = 13,
        Boolean = 14,
        Address = 15
    }
}

namespace UJAS.Core.Enums
{
    public enum FieldCategory
    {
        PersonalInformation = 1,
        ContactInformation = 2,
        DemographicInformation = 3,
        WorkAuthorization = 4,
        EmploymentPreferences = 5,
        Education = 6,
        LicensesCertifications = 7,
        WorkExperience = 8,
        MilitaryService = 9,
        Skills = 10,
        Documents = 11,
        BackgroundCheck = 12,
        References = 13,
        EmergencyContacts = 14,
        CompanySpecific = 15,
        Agreements = 16,
        System = 17
    }
}

namespace UJAS.Core.Enums
{
    public enum PrivacyLevel
    {
        Public = 1,
        Internal = 2,
        Confidential = 3,
        Restricted = 4,
        PII = 5,
        EEO = 6,
        GDPR = 7
    }
}

namespace UJAS.Core.Enums
{
    public enum AssessmentType
    {
        Personality = 1,
        SkillsTest = 2,
        Cognitive = 3,
        Situational = 4,
        VideoInterview = 5,
        Technical = 6,
        Language = 7
    }
}

namespace UJAS.Core.Enums
{
    public enum AssessmentStatus
    {
        NotStarted = 1,
        InProgress = 2,
        Completed = 3,
        Expired = 4,
        Evaluated = 5
    }
}