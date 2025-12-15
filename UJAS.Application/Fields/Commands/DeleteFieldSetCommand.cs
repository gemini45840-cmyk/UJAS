using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class DeleteFieldSetCommand : IRequest<bool>
    {
        public Guid FieldSetId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool SoftDelete { get; set; } = true;
        public bool DeleteFields { get; set; } = false;

        public DeleteFieldSetCommand(Guid fieldSetId, Guid deletedBy)
        {
            FieldSetId = fieldSetId;
            DeletedBy = deletedBy;
        }
    }
}