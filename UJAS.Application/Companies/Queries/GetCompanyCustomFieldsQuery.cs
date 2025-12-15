using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyCustomFieldsQuery : IRequest<List<CompanyCustomFieldDto>>
    {
        public Guid CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public string Section { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsVisibleToApplicant { get; set; }

        public GetCompanyCustomFieldsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}