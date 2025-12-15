using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class DuplicateFieldCommand : IRequest<Guid>
    {
        public Guid FieldId { get; set; }
        public string NewName { get; set; }
        public Guid? CompanyId { get; set; }
        public bool CopyOptions { get; set; } = true;
        public bool CopyValidation { get; set; } = true;
        public bool CopyConditionalLogic { get; set; } = true;
        public Guid DuplicatedBy { get; set; }

        public DuplicateFieldCommand(Guid fieldId, string newName, Guid duplicatedBy)
        {
            FieldId = fieldId;
            NewName = newName;
            DuplicatedBy = duplicatedBy;
        }
    }
}
