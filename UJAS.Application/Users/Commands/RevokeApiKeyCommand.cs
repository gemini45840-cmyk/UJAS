using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class RevokeApiKeyCommand : IRequest<bool>
    {
        public Guid ApiKeyId { get; set; }
        public Guid UserId { get; set; }
        public Guid RevokedBy { get; set; }
        public string Reason { get; set; }

        public RevokeApiKeyCommand(Guid apiKeyId, Guid userId, Guid revokedBy)
        {
            ApiKeyId = apiKeyId;
            UserId = userId;
            RevokedBy = revokedBy;
        }
    }
}