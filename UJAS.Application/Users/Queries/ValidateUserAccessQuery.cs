using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Queries
{
    public class ValidateUserAccessQuery : IRequest<UserAccessResult>
    {
        public Guid UserId { get; set; }
        public string RequiredPermission { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public string EntityType { get; set; }
        public Guid? EntityId { get; set; }

        public ValidateUserAccessQuery(Guid userId, string requiredPermission)
        {
            UserId = userId;
            RequiredPermission = requiredPermission;
        }
    }

    public class UserAccessResult
    {
        public bool HasAccess { get; set; }
        public string Reason { get; set; }
        public List<string> MissingPermissions { get; set; } = new();
        public Dictionary<string, object> Context { get; set; } = new();
        public DateTime CheckedAt { get; set; }
    }
}