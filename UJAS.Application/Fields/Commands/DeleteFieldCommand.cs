using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class DeleteFieldCommand : IRequest<bool>
    {
        public Guid FieldId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool SoftDelete { get; set; } = true;
        public bool ArchiveData { get; set; } = true;
        public Guid? ReplaceWithFieldId { get; set; }

        public DeleteFieldCommand(Guid fieldId, Guid deletedBy)
        {
            FieldId = fieldId;
            DeletedBy = deletedBy;
        }
    }
}
