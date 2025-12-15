using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class ImpersonateUserCommand : IRequest<LoginResponseDto>
    {
        public ImpersonateDto Impersonate { get; set; }
        public Guid ImpersonatedBy { get; set; }
        public string IpAddress { get; set; }

        public ImpersonateUserCommand(ImpersonateDto impersonate, Guid impersonatedBy)
        {
            Impersonate = impersonate;
            ImpersonatedBy = impersonatedBy;
        }
    }
}