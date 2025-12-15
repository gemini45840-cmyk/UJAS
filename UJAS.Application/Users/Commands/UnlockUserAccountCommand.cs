using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class UnlockUserAccountCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid UnlockedBy { get; set; }
        public string Reason { get; set; }

        public UnlockUserAccountCommand(Guid userId, Guid unlockedBy)
        {
            UserId = userId;
            UnlockedBy = unlockedBy;
        }
    }
}
