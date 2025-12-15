using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class DeleteCompanyCustomFieldCommand : IRequest<bool>
    {
        public Guid CustomFieldId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool ArchiveData { get; set; } = true;

        public DeleteCompanyCustomFieldCommand(Guid customFieldId, Guid deletedBy)
        {
            CustomFieldId = customFieldId;
            DeletedBy = deletedBy;
        }
    }
}