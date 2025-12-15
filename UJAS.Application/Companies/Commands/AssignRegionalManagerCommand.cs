using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class AssignRegionalManagerCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public List<Guid> LocationIds { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
        public Guid AssignedBy { get; set; }
        public bool SendInvitationEmail { get; set; } = true;

        public AssignRegionalManagerCommand(Guid companyId, string email, Guid assignedBy)
        {
            CompanyId = companyId;
            Email = email;
            AssignedBy = assignedBy;
        }
    }
}