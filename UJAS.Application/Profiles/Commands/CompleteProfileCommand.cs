using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class CompleteProfileCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public bool ForceCompletion { get; set; } = false;
        public Guid CompletedBy { get; set; }

        public CompleteProfileCommand(Guid profileId, Guid completedBy)
        {
            ProfileId = profileId;
            CompletedBy = completedBy;
        }
    }
}