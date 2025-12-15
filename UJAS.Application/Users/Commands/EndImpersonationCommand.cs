using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class EndImpersonationCommand : IRequest<LoginResponseDto>
    {
        public Guid UserId { get; set; }
        public string OriginalToken { get; set; }

        public EndImpersonationCommand(Guid userId, string originalToken)
        {
            UserId = userId;
            OriginalToken = originalToken;
        }
    }
}
