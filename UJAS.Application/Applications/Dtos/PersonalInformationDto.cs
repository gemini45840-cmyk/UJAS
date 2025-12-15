using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    // Personal Information Section
    public class PersonalInformationDto
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PreferredContactMethod { get; set; }
        public string BestTimeToContact { get; set; }

        // Address
        public AddressDto CurrentAddress { get; set; }
        public AddressDto MailingAddress { get; set; }
        public bool IsMailingSameAsCurrent { get; set; }
        public int YearsAtCurrentAddress { get; set; }
        public AddressDto PreviousAddress { get; set; }

        // Demographic (Voluntary)
        public string GenderIdentity { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EthnicityRace { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorization { get; set; }
    }

    public class AddressDto
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string AddressType { get; set; }
    }
}