using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class ExportUserDataCommand : IRequest<byte[]>
    {
        public Guid UserId { get; set; }
        public string ExportFormat { get; set; } = "JSON";
        public bool IncludePersonalInfo { get; set; } = true;
        public bool IncludeActivity { get; set; } = true;
        public bool IncludeAssociations { get; set; } = true;
        public bool IncludeSessions { get; set; } = true;
        public string Password { get; set; }
        public Guid RequestedBy { get; set; }

        public ExportUserDataCommand(Guid userId, Guid requestedBy)
        {
            UserId = userId;
            RequestedBy = requestedBy;
        }
    }
}
