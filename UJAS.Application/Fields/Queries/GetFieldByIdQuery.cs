using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldByIdQuery : IRequest<FieldDetailDto>
    {
        public Guid FieldId { get; set; }
        public bool IncludeDependencies { get; set; } = true;
        public bool IncludeUsage { get; set; } = true;
        public bool IncludeHistory { get; set; } = false;

        public GetFieldByIdQuery(Guid fieldId)
        {
            FieldId = fieldId;
        }
    }
}