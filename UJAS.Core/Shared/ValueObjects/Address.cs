using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Core.Shared.ValueObjects
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public Enums.AddressType Type { get; set; }
        public int? YearsAtAddress { get; set; }
        public bool IsMailingAddressSame { get; set; } = true;
        public Address MailingAddress { get; set; }
    }
}