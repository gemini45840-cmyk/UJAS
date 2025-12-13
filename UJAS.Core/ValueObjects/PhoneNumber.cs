namespace UJAS.Core.ValueObjects
{
    public class PhoneNumber
    {
        public string Number { get; private set; }
        public string Extension { get; private set; }
        public string CountryCode { get; private set; }

        private PhoneNumber() { }

        public PhoneNumber(string number, string countryCode = "1", string extension = null)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number is required");

            Number = FormatPhoneNumber(number);
            CountryCode = countryCode?.Trim();
            Extension = extension?.Trim();
        }

        private string FormatPhoneNumber(string number)
        {
            // Remove all non-digit characters
            var digits = new string(number.Where(char.IsDigit).ToArray());
            return digits.Length == 10 ?
                $"({digits[..3]}) {digits[3..6]}-{digits[6..]}" :
                digits;
        }

        public override string ToString()
        {
            var result = $"+{CountryCode} {Number}";
            if (!string.IsNullOrEmpty(Extension))
                result += $" ext. {Extension}";
            return result;
        }
    }
}