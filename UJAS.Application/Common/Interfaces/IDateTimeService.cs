namespace UJAS.Application.Common.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }
        DateTime ConvertToLocal(DateTime utcDateTime, string timeZoneId);
        DateTime ConvertToUtc(DateTime localDateTime, string timeZoneId);
    }

    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Today => DateTime.Today;

        public DateTime ConvertToLocal(DateTime utcDateTime, string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId))
                timeZoneId = "UTC";

            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                // Fallback to UTC
                return utcDateTime;
            }
        }

        public DateTime ConvertToUtc(DateTime localDateTime, string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId))
                timeZoneId = "UTC";

            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                // Assume the datetime is already in UTC
                return localDateTime;
            }
        }
    }
}