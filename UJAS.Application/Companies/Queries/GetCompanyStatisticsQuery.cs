using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyStatisticsQuery : IRequest<CompanyStatisticsDto>
    {
        public Guid CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? LocationId { get; set; }
        public bool IncludeLocationBreakdown { get; set; } = true;
        public bool IncludeApplicationTrends { get; set; } = true;
        public bool IncludeDemographicAnalytics { get; set; } = false;

        public GetCompanyStatisticsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class CompanyStatisticsDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }

        // Application Statistics
        public int TotalApplications { get; set; }
        public int NewApplications { get; set; }
        public int ApplicationsInProgress { get; set; }
        public int ApplicationsCompleted { get; set; }
        public int ApplicationsAccepted { get; set; }
        public int ApplicationsRejected { get; set; }
        public int ApplicationsWithdrawn { get; set; }
        public decimal ApplicationCompletionRate { get; set; }
        public decimal AverageApplicationTimeMinutes { get; set; }

        // Location Statistics
        public List<LocationStatisticsDto> LocationStatistics { get; set; } = new();

        // Position Statistics
        public List<PositionStatisticsDto> PositionStatistics { get; set; } = new();

        // Source Statistics
        public Dictionary<string, int> ApplicationsBySource { get; set; } = new();

        // Demographic Statistics (EEO)
        public DemographicStatisticsDto DemographicStatistics { get; set; }

        // Time Series Data
        public List<DailyStatisticsDto> DailyTrends { get; set; } = new();

        // Conversion Rates
        public ConversionRatesDto ConversionRates { get; set; }

        // Assessment Statistics
        public AssessmentStatisticsDto AssessmentStatistics { get; set; }
    }

    public class LocationStatisticsDto
    {
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public int TotalApplications { get; set; }
        public int NewApplications { get; set; }
        public int ApplicationsAccepted { get; set; }
        public decimal AcceptanceRate { get; set; }
        public decimal AverageApplicationTimeMinutes { get; set; }
    }

    public class PositionStatisticsDto
    {
        public string Position { get; set; }
        public int TotalApplications { get; set; }
        public int OpenPositions { get; set; }
        public int FilledPositions { get; set; }
        public decimal AverageApplicationsPerPosition { get; set; }
    }

    public class DemographicStatisticsDto
    {
        public Dictionary<string, int> GenderDistribution { get; set; } = new();
        public Dictionary<string, int> EthnicityDistribution { get; set; } = new();
        public Dictionary<string, int> VeteranStatusDistribution { get; set; } = new();
        public Dictionary<string, int> DisabilityStatusDistribution { get; set; } = new();
        public AgeDistributionDto AgeDistribution { get; set; }
    }

    public class AgeDistributionDto
    {
        public int Under18 { get; set; }
        public int Age18To24 { get; set; }
        public int Age25To34 { get; set; }
        public int Age35To44 { get; set; }
        public int Age45To54 { get; set; }
        public int Age55To64 { get; set; }
        public int Age65Plus { get; set; }
    }

    public class DailyStatisticsDto
    {
        public DateTime Date { get; set; }
        public int Applications { get; set; }
        public int Completions { get; set; }
        public int Acceptances { get; set; }
        public decimal CompletionRate { get; set; }
        public decimal AcceptanceRate { get; set; }
    }

    public class ConversionRatesDto
    {
        public decimal ApplicationToCompletion { get; set; }
        public decimal CompletionToAssessment { get; set; }
        public decimal AssessmentToInterview { get; set; }
        public decimal InterviewToOffer { get; set; }
        public decimal OfferToAcceptance { get; set; }
        public decimal OverallConversion { get; set; }
    }

    public class AssessmentStatisticsDto
    {
        public int TotalAssessments { get; set; }
        public int CompletedAssessments { get; set; }
        public int PassedAssessments { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public Dictionary<string, decimal> AssessmentTypePassRates { get; set; } = new();
    }
}