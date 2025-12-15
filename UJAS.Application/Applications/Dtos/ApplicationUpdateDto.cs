using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common.Services;
using UJAS.Application.Profiles.Dtos;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationUpdateDto
    {
        public Guid ApplicationId { get; set; }
        public ApplicationStatusDto? Status { get; set; }

        // Updateable sections
        public PersonalInformationDto PersonalInfo { get; set; }
        public EmploymentInformationDto EmploymentInfo { get; set; }
        public List<EducationHistoryDto> EducationHistory { get; set; }
        public List<WorkExperienceDto> WorkExperience { get; set; }
        public SkillsDto Skills { get; set; }
        public DocumentsDto Documents { get; set; }
        public BackgroundInformationDto BackgroundInfo { get; set; }
        public List<ReferenceDto> References { get; set; }
        public List<EmergencyContactDto> EmergencyContacts { get; set; }
        public List<CompanyQuestionResponseDto> CompanyQuestionResponses { get; set; }
        public AgreementsDto Agreements { get; set; }
    }
}