using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Storage.Monitoring;
using UJAS.Application.Common.Services;
using UJAS.Application.Profiles.Dtos;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationDetailDto
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid LocationId { get; set; }
        public Guid? PositionId { get; set; }
        public string Position { get; set; }
        public ApplicationStatusDto Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        // Personal Information
        public PersonalInformationDto PersonalInfo { get; set; }

        // Employment Information
        public EmploymentInformationDto EmploymentInfo { get; set; }

        // Education History
        public List<EducationHistoryDto> EducationHistory { get; set; } = new();

        // Work Experience
        public List<WorkExperienceDto> WorkExperience { get; set; } = new();

        // Skills
        public SkillsDto Skills { get; set; }

        // Documents
        public DocumentsDto Documents { get; set; }

        // Background Information
        public BackgroundInformationDto BackgroundInfo { get; set; }

        // References
        public List<ReferenceDto> References { get; set; } = new();

        // Emergency Contacts
        public List<EmergencyContactDto> EmergencyContacts { get; set; } = new();

        // Company Specific Questions
        public List<CompanyQuestionResponseDto> CompanyQuestionResponses { get; set; } = new();

        // Agreements
        public AgreementsDto Agreements { get; set; }

        // Application Metadata
        public ApplicationMetadataDto Metadata { get; set; }

        // Comments
        public List<ApplicationCommentDto> Comments { get; set; } = new();

        // Assessments
        public List<AssessmentResponseDto> AssessmentResponses { get; set; } = new();

        // Status History
        public List<StatusHistoryDto> StatusHistory { get; set; } = new();
    }
}
