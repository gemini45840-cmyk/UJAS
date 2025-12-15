using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<CompanyDetailDto>
    {
        public Guid CompanyId { get; set; }
        public bool IncludeLocations { get; set; } = true;
        public bool IncludeAdministrators { get; set; } = true;
        public bool IncludeSettings { get; set; } = true;
        public bool IncludeStatistics { get; set; } = true;
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }

        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}