using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileByUserIdQuery : IRequest<ProfileDetailDto>
    {
        public Guid UserId { get; set; }
        public bool IncludeSensitiveData { get; set; } = true;

        public GetProfileByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
