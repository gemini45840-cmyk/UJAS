using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class VerifyTwoFactorCommand : IRequest<LoginResponseDto>
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public string Method { get; set; }
        public bool RememberDevice { get; set; }
        public string DeviceId { get; set; }

        public VerifyTwoFactorCommand(Guid userId, string code, string method)
        {
            UserId = userId;
            Code = code;
            Method = method;
        }
    }
}