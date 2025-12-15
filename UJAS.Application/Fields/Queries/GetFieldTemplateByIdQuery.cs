using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldTemplateByIdQuery : IRequest<FieldTemplateDetailDto>
    {
        public Guid TemplateId { get; set; }
        public bool IncludeExamples { get; set; } = true;
        public bool IncludeReviews { get; set; } = true;

        public GetFieldTemplateByIdQuery(Guid templateId)
        {
            TemplateId = templateId;
        }
    }
}
