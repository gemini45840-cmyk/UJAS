using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class AddFieldDependencyCommand : IRequest<bool>
    {
        public Guid FieldId { get; set; }
        public Guid DependentFieldId { get; set; }
        public string DependencyType { get; set; }
        public string Condition { get; set; }
        public bool IsBidirectional { get; set; } = false;
        public Guid AddedBy { get; set; }

        public AddFieldDependencyCommand(Guid fieldId, Guid dependentFieldId, Guid addedBy)
        {
            FieldId = fieldId;
            DependentFieldId = dependentFieldId;
            AddedBy = addedBy;
        }
    }
}
