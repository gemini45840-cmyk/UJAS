using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class ExportProfileDataCommand : IRequest<byte[]>
    {
        public Guid ProfileId { get; set; }
        public string ExportFormat { get; set; } = "PDF"; // PDF, Word, JSON, XML, CSV
        public bool IncludePersonalInfo { get; set; } = true;
        public bool IncludeEducation { get; set; } = true;
        public bool IncludeExperience { get; set; } = true;
        public bool IncludeSkills { get; set; } = true;
        public bool IncludeDocuments { get; set; } = false;
        public bool IncludeApplications { get; set; } = false;
        public bool IncludeAssessments { get; set; } = false;
        public string Password { get; set; }
        public Guid RequestedBy { get; set; }

        public ExportProfileDataCommand(Guid profileId, Guid requestedBy)
        {
            ProfileId = profileId;
            RequestedBy = requestedBy;
        }
    }
}