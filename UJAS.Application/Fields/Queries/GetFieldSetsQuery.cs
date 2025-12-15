using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldSetsQuery : IRequest<PaginatedList<FieldSetDto>>
    {
        public Guid? CompanyId { get; set; }
        public string Category { get; set; }
        public bool? IsSystemSet { get; set; }
        public bool? IsActive { get; set; } = true;
        public string SearchTerm { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string SortBy { get; set; } = "Order";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public GetFieldSetsQuery(Guid? companyId = null)
        {
            CompanyId = companyId;
        }
    }
}
