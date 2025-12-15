using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldsQuery : IRequest<PaginatedList<FieldDto>>
    {
        public Guid? CompanyId { get; set; }
        public bool? IsGlobal { get; set; }
        public bool? IsSystemField { get; set; }
        public bool? IsActive { get; set; }
        public List<FieldTypeDto> Types { get; set; } = new();
        public List<FieldCategoryDto> Categories { get; set; } = new();
        public string Section { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsVisible { get; set; }
        public string SearchTerm { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string SortBy { get; set; } = "Order";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public GetFieldsQuery(Guid? companyId = null)
        {
            CompanyId = companyId;
        }
    }
}