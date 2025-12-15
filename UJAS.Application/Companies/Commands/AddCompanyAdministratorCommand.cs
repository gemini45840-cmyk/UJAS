using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class AddCompanyAdministratorCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsPrimary { get; set; } = false;
        public List<string> Permissions { get; set; } = new();
        public Guid AddedBy { get; set; }
        public bool SendInvitationEmail { get; set; } = true;

        public AddCompanyAdministratorCommand(Guid companyId, string email, Guid addedBy)
        {
            CompanyId = companyId;
            Email = email;
            AddedBy = addedBy;
        }
    }
}