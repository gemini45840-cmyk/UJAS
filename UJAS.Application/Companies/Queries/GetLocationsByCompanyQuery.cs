using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetLocationsByCompanyQuery : IRequest<PaginatedList<LocationDto>>
    {
        public Guid CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public string SearchTerm { get; set; }
        public List<string> Types { get; set; } = new();
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // For Regional Managers: filter by their managed locations
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }

        public GetLocationsByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
