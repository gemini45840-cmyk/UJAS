using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class DeleteProfileDocumentCommand : IRequest<bool>
    {
        public Guid DocumentId { get; set; }
        public Guid ProfileId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }

        public DeleteProfileDocumentCommand(Guid documentId, Guid profileId, Guid deletedBy)
        {
            DocumentId = documentId;
            ProfileId = profileId;
            DeletedBy = deletedBy;
        }
    }
}
