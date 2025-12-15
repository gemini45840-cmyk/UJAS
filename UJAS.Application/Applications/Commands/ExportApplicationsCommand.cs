using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class ExportApplicationsCommand : IRequest<byte[]>
    {
        public Guid CompanyId { get; set; }
        public List<Guid> ApplicationIds { get; set; }
        public string ExportFormat { get; set; } // CSV, Excel, PDF
        public List<string> FieldsToInclude { get; set; } = new();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IncludePII { get; set; }
        public bool IncludeDocuments { get; set; }

        public ExportApplicationsCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
