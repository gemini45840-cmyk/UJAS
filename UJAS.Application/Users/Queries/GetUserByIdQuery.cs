using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDetailDto>
    {
        public Guid UserId { get; set; }
        public Guid? RequestedBy { get; set; }
        public string RequestedByRole { get; set; }
        public bool IncludeSensitiveData { get; set; } = false;
        public bool IncludeActivity { get; set; } = true;
        public bool IncludeAssociations { get; set; } = true;

        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
