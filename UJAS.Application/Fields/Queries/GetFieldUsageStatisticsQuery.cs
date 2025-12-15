using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Queries
{
    public class GetFieldUsageStatisticsQuery : IRequest<FieldValueAnalysisDto>
    {
        public Guid FieldId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public bool IncludeTrends { get; set; } = true;
        public bool IncludeCommonValues { get; set; } = true;
        public bool IncludeErrorAnalysis { get; set; } = true;

        public GetFieldUsageStatisticsQuery(Guid fieldId)
        {
            FieldId = fieldId;
        }
    }
}
