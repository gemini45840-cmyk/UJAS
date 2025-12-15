using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class ExportAssessmentResultsCommand : IRequest<byte[]>
    {
        public Guid AssessmentId { get; set; }
        public List<Guid> ResponseIds { get; set; } = new();
        public string ExportFormat { get; set; } = "Excel";
        public List<string> FieldsToInclude { get; set; } = new();
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IncludeQuestionResponses { get; set; } = false;
        public bool IncludeProctoringData { get; set; } = false;
        public bool IncludeDemographics { get; set; } = false;

        public ExportAssessmentResultsCommand(Guid assessmentId)
        {
            AssessmentId = assessmentId;
        }
    }
}
