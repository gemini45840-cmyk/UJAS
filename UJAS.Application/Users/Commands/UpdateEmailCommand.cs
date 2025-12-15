using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Users.DTOs;

namespace UJAS.Application.Users.Commands
{
    public class UpdateEmailCommand : IRequest<bool>
    {
        public UserEmailUpdateDto EmailUpdate { get; set; }
        public Guid UpdatedBy { get; set; }
        public string IpAddress { get; set; }

        public UpdateEmailCommand(UserEmailUpdateDto emailUpdate, Guid updatedBy)
        {
            EmailUpdate = emailUpdate;
            UpdatedBy = updatedBy;
        }
    }
}