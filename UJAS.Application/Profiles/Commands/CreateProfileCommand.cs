using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Profiles.DTOs;

namespace UJAS.Application.Profiles.Commands
{
    public class CreateProfileCommand : IRequest<Guid>
    {
        public ProfileCreateDto Profile { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public bool SendWelcomeEmail { get; set; } = true;
        public bool VerifyEmail { get; set; } = true;

        public CreateProfileCommand(ProfileCreateDto profile)
        {
            Profile = profile;
        }
    }
}
