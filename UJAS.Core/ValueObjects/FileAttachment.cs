// UJAS.Core/ValueObjects/Address.cs
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

// UJAS.Core/ValueObjects/PhoneNumber.cs
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

// UJAS.Core/ValueObjects/SalaryRange.cs
namespace UJAS.Core.ValueObjects
{
    public class SalaryRange
    {
        public decimal? Minimum { get; private set; }
        public decimal? Maximum { get; private set; }
        public string Currency { get; private set; }
        public bool IsHourly { get; private set; }

        private SalaryRange() { }

        public SalaryRange(decimal? minimum, decimal? maximum, string currency = "USD", bool isHourly = false)
        {
            if (minimum.HasValue && maximum.HasValue && minimum > maximum)
                throw new ArgumentException("Minimum cannot be greater than maximum");

            Minimum = minimum;
            Maximum = maximum;
            Currency = currency ?? "USD";
            IsHourly = isHourly;
        }

        public string Display()
        {
            if (!Minimum.HasValue && !Maximum.HasValue)
                return "Negotiable";

            var type = IsHourly ? "per hour" : "per year";

            if (Minimum.HasValue && Maximum.HasValue)
                return $"{Currency} {Minimum.Value:N0} - {Maximum.Value:N0} {type}";
            else if (Minimum.HasValue)
                return $"{Currency} {Minimum.Value:N0}+ {type}";
            else
                return $"Up to {Currency} {Maximum.Value:N0} {type}";
        }
    }
}

// UJAS.Core/ValueObjects/DateRange.cs
namespace UJAS.Core.ValueObjects
{
    public class DateRange
    {
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsCurrent { get; private set; }

        private DateRange() { }

        public DateRange(DateTime startDate, DateTime? endDate = null, bool isCurrent = false)
        {
            if (endDate.HasValue && startDate > endDate.Value)
                throw new ArgumentException("Start date cannot be after end date");

            StartDate = startDate;
            EndDate = endDate;
            IsCurrent = isCurrent;
        }

        public int? GetDurationInMonths()
        {
            if (IsCurrent || !EndDate.HasValue)
                return null;

            var months = ((EndDate.Value.Year - StartDate.Year) * 12) +
                        EndDate.Value.Month - StartDate.Month;
            return Math.Max(months, 0);
        }
    }
}

// UJAS.Core/ValueObjects/FileAttachment.cs
namespace UJAS.Core.ValueObjects
{
    public class FileAttachment
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string ContentType { get; private set; }
        public long FileSize { get; private set; }
        public DateTime UploadDate { get; private set; }
        public string UploadedBy { get; private set; }

        private FileAttachment() { }

        public FileAttachment(string fileName, string filePath, string contentType,
                            long fileSize, string uploadedBy)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name is required");
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required");
            if (fileSize <= 0)
                throw new ArgumentException("File size must be positive");

            FileName = fileName.Trim();
            FilePath = filePath.Trim();
            ContentType = contentType?.Trim() ?? "application/octet-stream";
            FileSize = fileSize;
            UploadDate = DateTime.UtcNow;
            UploadedBy = uploadedBy ?? "system";
        }

        public string GetFileSizeDisplay()
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = FileSize;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}