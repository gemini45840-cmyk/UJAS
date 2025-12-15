using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.Dtos;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationCreateDto
    {
        public Guid CompanyId { get; set; }
        public Guid LocationId { get; set; }
        public string Position { get; set; }
        public Guid? PositionId { get; set; }
        public ApplicationSourceDto Source { get; set; } = ApplicationSourceDto.Widget;
        public string ReferralSource { get; set; }
        public string CampaignCode { get; set; }

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
    }
}