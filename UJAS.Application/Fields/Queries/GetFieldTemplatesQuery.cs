using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldTemplatesQuery : IRequest<PaginatedList<FieldTemplateDto>>
    {
        public string Category { get; set; }
        public FieldTypeDto? FieldType { get; set; }
        public string Industry { get; set; }
        public List<string> JobRoles { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public bool? IsPublic { get; set; } = true;
        public bool? IsSystemTemplate { get; set; }
        public decimal? MinRating { get; set; }
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "UsageCount";
        public bool SortDescending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
