using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class GenerateWidgetEmbedCodeCommand : IRequest<string>
    {
        public Guid CompanyId { get; set; }
        public Guid GeneratedBy { get; set; }
        public bool RegenerateApiKey { get; set; } = false;
        public DateTime? ExpirationDate { get; set; }

        public GenerateWidgetEmbedCodeCommand(Guid companyId, Guid generatedBy)
        {
            CompanyId = companyId;
            GeneratedBy = generatedBy;
        }
    }
}