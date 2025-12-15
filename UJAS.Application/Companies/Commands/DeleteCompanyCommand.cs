using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool PermanentDelete { get; set; } = false;
        public bool ArchiveData { get; set; } = true;

        public DeleteCompanyCommand(Guid companyId, Guid deletedBy)
        {
            CompanyId = companyId;
            DeletedBy = deletedBy;
        }
    }
}
