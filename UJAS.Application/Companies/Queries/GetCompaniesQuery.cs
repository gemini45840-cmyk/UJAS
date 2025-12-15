using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompaniesQuery : IRequest<PaginatedList<CompanyDto>>
    {
        public string SearchTerm { get; set; }
        public List<string> Industries { get; set; } = new();
        public List<string> Statuses { get; set; } = new();
        public List<string> SizeCategories { get; set; } = new();
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public bool? IsActive { get; set; }
        public string SortBy { get; set; } = "Name";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        // For System Admin: show all companies
        // For Company Admin: show only their company
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }
    }
}
