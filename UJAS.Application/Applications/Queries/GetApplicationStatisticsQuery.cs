using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Queries
{
    public class GetApplicationStatisticsQuery : IRequest<ApplicationStatisticsDto>
    {
        public Guid CompanyId { get; set; }
        public Guid? LocationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Position { get; set; }

        public GetApplicationStatisticsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class ApplicationStatisticsDto
    {
        public int TotalApplications { get; set; }
        public int NewApplications { get; set; }
        public int UnderReview { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
        public int Withdrawn { get; set; }
        public decimal AverageCompletionTimeMinutes { get; set; }
        public Dictionary<string, int> ApplicationsBySource { get; set; } = new();
        public Dictionary<string, int> ApplicationsByPosition { get; set; } = new();
        public Dictionary<string, int> ApplicationsByLocation { get; set; } = new();
        public List<DailyApplicationCountDto> DailyTrends { get; set; } = new();
        public decimal ConversionRate { get; set; } // Applications to Accepted
        public List<StatusTransitionDto> StatusTransitions { get; set; } = new();
    }

    public class DailyApplicationCountDto
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class StatusTransitionDto
    {
        public string FromStatus { get; set; }
        public string ToStatus { get; set; }
        public int Count { get; set; }
        public decimal AverageDurationHours { get; set; }
    }
}
