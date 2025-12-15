using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class ImportAssessmentTemplateCommand : IRequest<Guid>
    {
        public Guid TemplateId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ImportedBy { get; set; }
        public string CustomName { get; set; }
        public bool CustomizeQuestions { get; set; } = false;
        public bool IncludeScoring { get; set; } = true;
        public bool IncludeSettings { get; set; } = true;

        public ImportAssessmentTemplateCommand(Guid templateId, Guid companyId, Guid importedBy)
        {
            TemplateId = templateId;
            CompanyId = companyId;
            ImportedBy = importedBy;
        }
    }
}
