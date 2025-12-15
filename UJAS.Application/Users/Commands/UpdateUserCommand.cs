using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserUpdateDto User { get; set; }
        public Guid UpdatedBy { get; set; }
        public string IpAddress { get; set; }

        public UpdateUserCommand(UserUpdateDto user, Guid updatedBy)
        {
            User = user;
            UpdatedBy = updatedBy;
        }
    }
}