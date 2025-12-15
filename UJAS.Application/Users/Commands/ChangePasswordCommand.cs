using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public UserPasswordChangeDto PasswordChange { get; set; }
        public Guid ChangedBy { get; set; }
        public string IpAddress { get; set; }

        public ChangePasswordCommand(UserPasswordChangeDto passwordChange, Guid changedBy)
        {
            PasswordChange = passwordChange;
            ChangedBy = changedBy;
        }
    }
}
