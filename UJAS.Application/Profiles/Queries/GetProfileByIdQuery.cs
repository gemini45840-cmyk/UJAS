using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileByIdQuery : IRequest<ProfileDetailDto>
    {
        public Guid ProfileId { get; set; }
        public Guid? RequestedBy { get; set; }
        public string RequestedByRole { get; set; }
        public bool IncludeSensitiveData { get; set; } = false;
        public bool IncludeDocuments { get; set; } = true;
        public bool IncludeHistory { get; set; } = true;

        public GetProfileByIdQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}
