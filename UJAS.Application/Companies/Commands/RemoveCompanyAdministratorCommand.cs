using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class RemoveCompanyAdministratorCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid RemovedBy { get; set; }
        public string Reason { get; set; }
        public bool SendNotificationEmail { get; set; } = true;

        public RemoveCompanyAdministratorCommand(Guid companyId, Guid userId, Guid removedBy)
        {
            CompanyId = companyId;
            UserId = userId;
            RemovedBy = removedBy;
        }
    }
}