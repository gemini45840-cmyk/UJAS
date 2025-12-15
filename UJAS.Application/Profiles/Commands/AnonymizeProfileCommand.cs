using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class AnonymizeProfileCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public AnonymizationLevel Level { get; set; } = AnonymizationLevel.Partial;
        public bool ArchiveOriginal { get; set; } = true;
        public Guid AnonymizedBy { get; set; }
        public string Reason { get; set; }

        public AnonymizeProfileCommand(Guid profileId, Guid anonymizedBy)
        {
            ProfileId = profileId;
            AnonymizedBy = anonymizedBy;
        }
    }

    public enum AnonymizationLevel
    {
        Partial,    // Remove direct identifiers only
        Full,       // Remove all PII
        Aggressive  // Remove all data, keep only metadata
    }
}
