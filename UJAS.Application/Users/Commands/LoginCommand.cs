using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public LoginDto Login { get; set; }

        public LoginCommand(LoginDto login)
        {
            Login = login;
        }
    }
}
