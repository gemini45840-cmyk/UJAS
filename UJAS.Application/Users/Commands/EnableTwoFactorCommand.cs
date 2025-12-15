using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class EnableTwoFactorCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string Method { get; set; } // Email, Phone, Authenticator
        public string PhoneNumber { get; set; }
        public string AuthenticatorKey { get; set; }
        public string VerificationCode { get; set; }
        public Guid EnabledBy { get; set; }

        public EnableTwoFactorCommand(Guid userId, string method, Guid enabledBy)
        {
            UserId = userId;
            Method = method;
            EnabledBy = enabledBy;
        }
    }
}
