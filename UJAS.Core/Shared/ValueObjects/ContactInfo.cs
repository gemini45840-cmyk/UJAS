using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Core.Shared.ValueObjects
{
    public class ContactInfo
    {
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public Enums.ContactMethod PreferredContactMethod { get; set; }
        public Enums.BestTimeToContact BestTimeToContact { get; set; }
    }
}