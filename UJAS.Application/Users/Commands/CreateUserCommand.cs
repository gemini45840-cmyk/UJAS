using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public UserCreateDto User { get; set; }
        public Guid CreatedBy { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public CreateUserCommand(UserCreateDto user, Guid createdBy)
        {
            User = user;
            CreatedBy = createdBy;
        }
    }
}