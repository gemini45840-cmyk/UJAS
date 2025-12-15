using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class ImportProfileDataCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public string ImportSource { get; set; } // LinkedIn, Indeed, Monster, CSV, JSON
        public string Data { get; set; } // JSON/XML/CSV data or access token
        public ImportOptionsDto Options { get; set; }
        public Guid ImportedBy { get; set; }

        public ImportProfileDataCommand(Guid profileId, string importSource, Guid importedBy)
        {
            ProfileId = profileId;
            ImportSource = importSource;
            ImportedBy = importedBy;
        }
    }

    public class ImportOptionsDto
    {
        public bool ImportPersonalInfo { get; set; } = true;
        public bool ImportEducation { get; set; } = true;
        public bool ImportExperience { get; set; } = true;
        public bool ImportSkills { get; set; } = true;
        public bool ImportCertifications { get; set; } = true;
        public bool OverrideExistingData { get; set; } = false;
        public bool ValidateData { get; set; } = true;
        public bool SendNotification { get; set; } = true;
    }
}
