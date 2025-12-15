using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class RemoveFieldDependencyCommand : IRequest<bool>
    {
        public Guid FieldId { get; set; }
        public Guid DependentFieldId { get; set; }
        public Guid RemovedBy { get; set; }
        public string Reason { get; set; }

        public RemoveFieldDependencyCommand(Guid fieldId, Guid dependentFieldId, Guid removedBy)
        {
            FieldId = fieldId;
            DependentFieldId = dependentFieldId;
            RemovedBy = removedBy;
        }
    }
}
