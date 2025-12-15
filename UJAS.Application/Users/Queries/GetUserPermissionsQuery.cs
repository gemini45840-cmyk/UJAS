using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Queries
{
    public class GetUserPermissionsQuery : IRequest<List<string>>
    {
        public Guid UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public bool IncludeInherited { get; set; } = true;

        public GetUserPermissionsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
