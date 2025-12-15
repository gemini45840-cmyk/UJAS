using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResponseDto>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public RefreshTokenCommand(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
