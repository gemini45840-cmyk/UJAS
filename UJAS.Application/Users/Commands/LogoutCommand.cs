using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class LogoutCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public bool LogoutAllDevices { get; set; } = false;
        public string Reason { get; set; }

        public LogoutCommand(string token, Guid userId)
        {
            Token = token;
            UserId = userId;
        }
    }
}
