using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldDependenciesQuery : IRequest<List<FieldDependencyDto>>
    {
        public Guid FieldId { get; set; }
        public string DependencyType { get; set; }
        public bool IncludeInverse { get; set; } = true;

        public GetFieldDependenciesQuery(Guid fieldId)
        {
            FieldId = fieldId;
        }
    }
}