using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
        public bool IncludeSensitiveData { get; set; } = false;

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}