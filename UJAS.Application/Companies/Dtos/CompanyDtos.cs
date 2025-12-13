namespace UJAS.Application.Companies.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string WidgetEmbedCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string TimeZone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CompanySettingsDto Settings { get; set; }
        public List<LocationDto> Locations { get; set; } = new();
        public CompanyStatisticsDto Statistics { get; set; }
    }

    public class CompanySettingsDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public bool RequireAssessment { get; set; }
        public bool EnableBackgroundCheck { get; set; }
        public bool EnableDrugTest { get; set; }
        public int ApplicationRetentionDays { get; set; }
        public bool AutoReplyToApplicants { get; set; }
        public string AutoReplyMessage { get; set; }
        public bool AllowApplicationWithdraw { get; set; }
        public bool ShowSalaryFields { get; set; }
        public bool CollectEEOData { get; set; }
        public string DefaultLanguage { get; set; }
        public string DateFormat { get; set; }
    }

    public class CreateCompanyDto
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TimeZone { get; set; }

        public CompanySettingsDto Settings { get; set; }
    }

    public class UpdateCompanyDto
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public bool IsActive { get; set; }
        public string TimeZone { get; set; }

        public CompanySettingsDto Settings { get; set; }
    }

    public class CompanyStatisticsDto
    {
        public int TotalLocations { get; set; }
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int HiredCount { get; set; }
        public double AverageHireTime { get; set; } // in days
        public Dictionary<string, int> ApplicationsByLocation { get; set; } = new();
        public Dictionary<string, int> ApplicationsByMonth { get; set; } = new();
    }

    public class CompanyWidgetSettingsDto
    {
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string LogoUrl { get; set; }
        public string CompanyName { get; set; }
        public bool ShowLocationSelector { get; set; }
        public bool ShowPositionField { get; set; }
        public List<LocationOptionDto> Locations { get; set; } = new();
        public List<string> AvailablePositions { get; set; } = new();
    }

    public class LocationOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
    }
}