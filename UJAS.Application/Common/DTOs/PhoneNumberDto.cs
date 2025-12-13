namespace UJAS.Application.Common.DTOs
{
    public class PhoneNumberDto
    {
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; } = "1";
    }
}