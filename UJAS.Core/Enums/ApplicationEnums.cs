using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Core.Enums
{
    public enum ApplicationStatus
    {
        Draft = 1,
        Submitted = 2,
        UnderReview = 3,
        Shortlisted = 4,
        InterviewScheduled = 5,
        AssessmentPending = 6,
        AssessmentCompleted = 7,
        Accepted = 8,
        Rejected = 9,
        Withdrawn = 10,
        OnHold = 11
    }

    public enum FieldType
    {
        Text,
        TextArea,
        Number,
        Date,
        Dropdown,
        MultiSelect,
        RadioButtons,
        FileUpload,
        Email,
        Phone,
        Url,
        Signature,
        RatingScale,
        YesNoToggle,
        AddressGroup
    }

    public enum FieldCategory
    {
        PersonalInformation,
        EmploymentInformation,
        EducationHistory,
        WorkExperience,
        SkillsQualifications,
        DocumentsAttachments,
        BackgroundCheck,
        EmergencyContact,
        CompanySpecific,
        Assessments,
        Agreements,
        Metadata
    }

    public enum PrivacyFlag
    {
        PII,
        EEO,
        GDPR_Sensitive,
        BackgroundCheckData,
        EncryptionRequired
    }

    public enum DocumentType
    {
        Resume,
        CoverLetter,
        Portfolio,
        WritingSample,
        DesignSample,
        CodeSample,
        Certificate,
        Degree,
        License,
        RecommendationLetter,
        Transcript,
        Headshot,
        DriversLicense,
        DD214,
        Other
    }

    public enum ReferralSource
    {
        CompanyWebsite,
        Indeed,
        LinkedIn,
        Referral,
        JobFair,
        SocialMedia,
        Other
    }

    public enum RejectionReason
    {
        Qualifications,
        Experience,
        Skills,
        Availability,
        Location,
        SalaryExpectations,
        PositionFilled,
        CompanyNotHiring,
        Other
    }
}