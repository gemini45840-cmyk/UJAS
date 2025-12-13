namespace UJAS.Core.Helpers
{
    public static class ApplicationNumberGenerator
    {
        public static string Generate(int companyId, int sequenceNumber)
        {
            var date = DateTime.UtcNow;
            var year = date.ToString("yy");
            var month = date.ToString("MM");
            var companyCode = companyId.ToString("D4");
            var sequence = sequenceNumber.ToString("D6");

            return $"APP-{companyCode}-{year}{month}-{sequence}";
        }
    }
}