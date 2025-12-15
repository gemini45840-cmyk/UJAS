using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanyAdministratorPermissionsCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public List<string> Permissions { get; set; } = new();
        public Guid UpdatedBy { get; set; }
        public string UpdateReason { get; set; }

        public UpdateCompanyAdministratorPermissionsCommand(Guid companyId, Guid userId, Guid updatedBy)
        {
            CompanyId = companyId;
            UserId = userId;
            UpdatedBy = updatedBy;
        }
    }
}
