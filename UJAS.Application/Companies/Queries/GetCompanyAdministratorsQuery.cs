using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyAdministratorsQuery : IRequest<List<CompanyAdministratorDto>>
    {
        public Guid CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }

        public GetCompanyAdministratorsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
