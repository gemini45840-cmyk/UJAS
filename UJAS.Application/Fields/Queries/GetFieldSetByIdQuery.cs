using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldSetByIdQuery : IRequest<FieldSetDetailDto>
    {
        public Guid FieldSetId { get; set; }
        public bool IncludeFields { get; set; } = true;
        public bool IncludeHistory { get; set; } = false;
        public bool IncludeUsage { get; set; } = false;

        public GetFieldSetByIdQuery(Guid fieldSetId)
        {
            FieldSetId = fieldSetId;
        }
    }
}
