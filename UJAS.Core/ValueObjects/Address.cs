using System;
using UJAS.Core.Enums;

namespace UJAS.Core.ValueObjects
{
    public class Address
    {
        public string Line1 { get; private set; }
        public string Line2 { get; private set; }
        public string City { get; private set; }
        public string StateProvince { get; private set; }
        public string ZipPostalCode { get; private set; }
        public string Country { get; private set; }
        public AddressType Type { get; private set; }
        public int? YearsAtAddress { get; private set; }

        private Address() { }

        public Address(string line1, string city, string stateProvince, string zipPostalCode,
                      string country, AddressType type = AddressType.Home,
                      string line2 = null, int? yearsAtAddress = null)
        {
            if (string.IsNullOrWhiteSpace(line1))
                throw new ArgumentException("Address line 1 is required");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required");

            Line1 = line1.Trim();
            Line2 = line2?.Trim();
            City = city.Trim();
            StateProvince = stateProvince?.Trim();
            ZipPostalCode = zipPostalCode?.Trim();
            Country = country.Trim();
            Type = type;
            YearsAtAddress = yearsAtAddress;
        }
    }
}
