using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class ParseResumeCommand : IRequest<ResumeParseResult>
    {
        public Guid ProfileId { get; set; }
        public Guid DocumentId { get; set; }
        public bool UpdateProfile { get; set; } = true;
        public bool OverrideExistingData { get; set; } = false;

        public ParseResumeCommand(Guid profileId, Guid documentId)
        {
            ProfileId = profileId;
            DocumentId = documentId;
        }
    }

    public class ResumeParseResult
    {
        public Guid ProfileId { get; set; }
        public Guid DocumentId { get; set; }
        public bool Success { get; set; }
        public string ParserUsed { get; set; }
        public DateTime ParseDate { get; set; }
        public ParsedDataDto ParsedData { get; set; }
        public List<string> Warnings { get; set; } = new();
        public List<string> Errors { get; set; } = new();
        public int FieldsExtracted { get; set; }
        public int FieldsUpdated { get; set; }
    }

    public class ParsedDataDto
    {
        public PersonalInformationDto PersonalInformation { get; set; }
        public List<EducationHistoryDto> EducationHistory { get; set; } = new();
        public List<WorkExperienceDto> WorkExperience { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<string> Certifications { get; set; } = new();
        public string Summary { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; } = new();
    }
}
