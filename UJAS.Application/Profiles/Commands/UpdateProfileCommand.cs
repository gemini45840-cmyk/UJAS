using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Commands
{
    public class UpdateProfileCommand : IRequest<bool>
    {
        public ProfileUpdateDto Profile { get; set; }
        public Guid UpdatedBy { get; set; }
        public string IpAddress { get; set; }
        public bool ValidateChanges { get; set; } = true;
        public bool SendNotification { get; set; } = true;

        public UpdateProfileCommand(ProfileUpdateDto profile, Guid updatedBy)
        {
            Profile = profile;
            UpdatedBy = updatedBy;
        }
    }
}
