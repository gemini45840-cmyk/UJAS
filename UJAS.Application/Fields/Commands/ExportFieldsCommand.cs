using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class ExportFieldsCommand : IRequest<byte[]>
    {
        public Guid? CompanyId { get; set; }
        public string ExportFormat { get; set; } = "JSON"; // JSON, XML, CSV, Excel
        public List<Guid> FieldIds { get; set; } = new();
        public bool IncludeFieldSets { get; set; } = false;
        public bool IncludeTemplates { get; set; } = false;
        public bool IncludeValidationRules { get; set; } = true;
        public bool IncludeOptions { get; set; } = true;
        public Guid RequestedBy { get; set; }

        public ExportFieldsCommand(Guid? companyId, Guid requestedBy)
        {
            CompanyId = companyId;
            RequestedBy = requestedBy;
        }
    }
}