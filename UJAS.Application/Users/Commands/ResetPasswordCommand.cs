using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public UserPasswordResetDto PasswordReset { get; set; }
        public string IpAddress { get; set; }

        public ResetPasswordCommand(UserPasswordResetDto passwordReset)
        {
            PasswordReset = passwordReset;
        }
    }
}