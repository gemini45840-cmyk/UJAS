using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class DisableTwoFactorCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string VerificationCode { get; set; }
        public Guid DisabledBy { get; set; }
        public string Reason { get; set; }

        public DisableTwoFactorCommand(Guid userId, Guid disabledBy)
        {
            UserId = userId;
            DisabledBy = disabledBy;
        }
    }
}
