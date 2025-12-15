using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldsBySectionQuery : IRequest<List<FieldDto>>
    {
        public Guid? CompanyId { get; set; }
        public string Section { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool IncludeConditionalFields { get; set; } = true;
        public Guid? ForApplicationId { get; set; }

        public GetFieldsBySectionQuery(string section)
        {
            Section = section;
        }
    }
}
