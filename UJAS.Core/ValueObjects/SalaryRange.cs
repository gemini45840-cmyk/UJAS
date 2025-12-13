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