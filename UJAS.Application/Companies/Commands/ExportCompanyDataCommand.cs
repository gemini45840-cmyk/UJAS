using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class ExportCompanyDataCommand : IRequest<byte[]>
    {
        public Guid CompanyId { get; set; }
        public string ExportFormat { get; set; } = "Excel";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IncludeApplications { get; set; } = true;
        public bool IncludeAssessments { get; set; } = true;
        public bool IncludeUsers { get; set; } = true;
        public bool IncludeAnalytics { get; set; } = true;
        public bool IncludePII { get; set; } = false;
        public string Password { get; set; }
        public Guid RequestedBy { get; set; }

        public ExportCompanyDataCommand(Guid companyId, Guid requestedBy)
        {
            CompanyId = companyId;
            RequestedBy = requestedBy;
        }
    }
}