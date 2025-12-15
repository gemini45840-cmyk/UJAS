using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class VerifyProfileCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public string VerificationType { get; set; } // Email, Phone, Identity, Education, Experience
        public string VerificationCode { get; set; }
        public Guid VerifiedBy { get; set; }
        public string Notes { get; set; }

        public VerifyProfileCommand(Guid profileId, string verificationType, Guid verifiedBy)
        {
            ProfileId = profileId;
            VerificationType = verificationType;
            VerifiedBy = verifiedBy;
        }
    }
}