using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public UserRegisterDto User { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public RegisterUserCommand(UserRegisterDto user)
        {
            User = user;
        }
    }
}