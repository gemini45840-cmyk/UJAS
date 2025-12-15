using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class SearchFieldsQuery : IRequest<PaginatedList<FieldDto>>
    {
        public Guid? CompanyId { get; set; }
        public string SearchTerm { get; set; }
        public List<FieldTypeDto> Types { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> Sections { get; set; } = new();
        public bool? IsRequired { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? HasOptions { get; set; }
        public bool? HasConditionalLogic { get; set; }
        public bool? IsPII { get; set; }
        public DateTime? UpdatedAfter { get; set; }
        public DateTime? UpdatedBefore { get; set; }
        public string SortBy { get; set; } = "Relevance";
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public SearchFieldsQuery(Guid? companyId = null)
        {
            CompanyId = companyId;
        }
    }
}
