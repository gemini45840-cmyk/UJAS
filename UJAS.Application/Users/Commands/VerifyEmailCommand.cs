using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class VerifyEmailCommand : IRequest<bool>
    {
        public VerifyEmailDto VerifyEmail { get; set; }
        public string IpAddress { get; set; }

        public VerifyEmailCommand(VerifyEmailDto verifyEmail)
        {
            VerifyEmail = verifyEmail;
        }
    }
}
