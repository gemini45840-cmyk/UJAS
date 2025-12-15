using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public ForgotPasswordDto ForgotPassword { get; set; }
        public string IpAddress { get; set; }

        public ForgotPasswordCommand(ForgotPasswordDto forgotPassword)
        {
            ForgotPassword = forgotPassword;
        }
    }
}