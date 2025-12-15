using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class SkillsDto
    {
        // Technical Skills
        public List<SkillProficiencyDto> SoftwareProficiency { get; set; } = new();
        public List<SkillProficiencyDto> ProgrammingLanguages { get; set; } = new();
        public List<string> OperatingSystems { get; set; } = new();
        public List<string> HardwareEquipment { get; set; } = new();
        public List<string> ToolsMachinery { get; set; } = new();
        public List<string> LaboratoryEquipment { get; set; } = new();
        public List<string> MedicalEquipment { get; set; } = new();
        public List<string> IndustrySpecificSoftware { get; set; } = new();

        // Professional Skills
        public List<LanguageProficiencyDto> LanguagesSpoken { get; set; } = new();
        public List<string> CommunicationSkills { get; set; } = new();
        public bool HasLeadershipExperience { get; set; }
        public bool HasProjectManagementExperience { get; set; }
        public bool HasCustomerServiceExperience { get; set; }
        public bool HasSalesExperience { get; set; }
        public bool HasTeachingExperience { get; set; }
        public bool HasResearchSkills { get; set; }
        public bool HasAnalyticalSkills { get; set; }

        // Soft Skills
        public List<string> SoftSkills { get; set; } = new();
    }

    public class SkillProficiencyDto
    {
        public string Name { get; set; }
        public string ProficiencyLevel { get; set; } // Beginner, Intermediate, Advanced, Expert
        public int? YearsOfExperience { get; set; }
    }

    public class LanguageProficiencyDto
    {
        public string Language { get; set; }
        public string Proficiency { get; set; } // Native, Fluent, Conversational, Basic
    }
}