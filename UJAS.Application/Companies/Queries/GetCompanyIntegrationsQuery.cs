using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyIntegrationsQuery : IRequest<IntegrationSettingsDto>
    {
        public Guid CompanyId { get; set; }
        public bool IncludeSecrets { get; set; } = false;

        public GetCompanyIntegrationsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}