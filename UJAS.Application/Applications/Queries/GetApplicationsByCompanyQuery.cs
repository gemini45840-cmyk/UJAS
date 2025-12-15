using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;
using UJAS.Application.Common;

namespace UJAS.Application.Applications.Queries
{
    public class GetApplicationsByCompanyQuery : IRequest<PaginatedList<ApplicationDto>>
    {
        public Guid CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public string Position { get; set; }
        public List<string> Statuses { get; set; } = new();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "ApplicationDate";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // Filter by custom field values
        public Dictionary<string, string> CustomFilters { get; set; } = new();

        public GetApplicationsByCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}