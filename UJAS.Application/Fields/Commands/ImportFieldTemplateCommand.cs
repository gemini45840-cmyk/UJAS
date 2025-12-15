using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class ImportFieldTemplateCommand : IRequest<Guid>
    {
        public Guid TemplateId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid ImportedBy { get; set; }
        public string CustomName { get; set; }
        public bool CustomizeField { get; set; } = false;
        public bool IncludeValidation { get; set; } = true;
        public bool IncludeOptions { get; set; } = true;

        public ImportFieldTemplateCommand(Guid templateId, Guid importedBy)
        {
            TemplateId = templateId;
            ImportedBy = importedBy;
        }
    }
}
