using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class SendProfileVerificationCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public string VerificationType { get; set; } // Email, Phone, Identity
        public string DeliveryMethod { get; set; } = "Email";
        public Guid SentBy { get; set; }

        public SendProfileVerificationCommand(Guid profileId, string verificationType, Guid sentBy)
        {
            ProfileId = profileId;
            VerificationType = verificationType;
            SentBy = sentBy;
        }
    }
}
