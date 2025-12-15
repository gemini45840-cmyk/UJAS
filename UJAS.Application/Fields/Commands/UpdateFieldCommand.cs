using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class UpdateFieldCommand : IRequest<bool>
    {
        public FieldUpdateDto Field { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdateReason { get; set; }

        public UpdateFieldCommand(FieldUpdateDto field, Guid updatedBy)
        {
            Field = field;
            UpdatedBy = updatedBy;
        }
    }
}