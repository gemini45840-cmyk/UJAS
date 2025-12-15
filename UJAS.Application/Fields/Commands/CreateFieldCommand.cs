using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class CreateFieldCommand : IRequest<Guid>
    {
        public FieldCreateDto Field { get; set; }
        public Guid CreatedBy { get; set; }
        public bool ValidateBeforeCreate { get; set; } = true;
        public bool PublishImmediately { get; set; } = true;

        public CreateFieldCommand(FieldCreateDto field, Guid createdBy)
        {
            Field = field;
            CreatedBy = createdBy;
        }
    }
}
