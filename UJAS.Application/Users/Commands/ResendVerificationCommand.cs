using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class ResendVerificationCommand : IRequest<bool>
    {
        public ResendVerificationDto ResendVerification { get; set; }
        public string IpAddress { get; set; }

        public ResendVerificationCommand(ResendVerificationDto resendVerification)
        {
            ResendVerification = resendVerification;
        }
    }
}
