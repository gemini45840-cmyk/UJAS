using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class CreateFieldSetCommand : IRequest<Guid>
    {
        public FieldSetCreateDto FieldSet { get; set; }
        public Guid CreatedBy { get; set; }

        public CreateFieldSetCommand(FieldSetCreateDto fieldSet, Guid createdBy)
        {
            FieldSet = fieldSet;
            CreatedBy = createdBy;
        }
    }
}
