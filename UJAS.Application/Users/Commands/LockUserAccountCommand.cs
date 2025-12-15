using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class LockUserAccountCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public string Reason { get; set; }
        public Guid LockedBy { get; set; }

        public LockUserAccountCommand(Guid userId, string reason, Guid lockedBy)
        {
            UserId = userId;
            Reason = reason;
            LockedBy = lockedBy;
        }
    }
}