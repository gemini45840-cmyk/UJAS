namespace UJAS.Application.Common.DTOs
{
    public class AddressDto
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string AddressType { get; set; }
        public int? YearsAtAddress { get; set; }
    }
}