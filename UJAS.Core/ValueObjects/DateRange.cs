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
