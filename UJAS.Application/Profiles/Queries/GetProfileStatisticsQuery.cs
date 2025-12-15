using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileStatisticsQuery : IRequest<ProfileStatisticsDto>
    {
        public Guid ProfileId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public GetProfileStatisticsQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }

    public class ProfileStatisticsDto
    {
        public Guid ProfileId { get; set; }

        // Application Statistics
        public int TotalApplications { get; set; }
        public int ApplicationsThisMonth { get; set; }
        public int ApplicationsThisYear { get; set; }
        public int AcceptedApplications { get; set; }
        public int RejectedApplications { get; set; }
        public int WithdrawnApplications { get; set; }
        public decimal ApplicationSuccessRate { get; set; }

        // Assessment Statistics
        public int TotalAssessments { get; set; }
        public int CompletedAssessments { get; set; }
        public int PassedAssessments { get; set; }
        public decimal AverageAssessmentScore { get; set; }

        // Profile Statistics
        public int ProfileViews { get; set; }
        public int CompaniesViewedProfile { get; set; }
        public DateTime? LastProfileView { get; set; }
        public int ProfileUpdates { get; set; }
        public DateTime? LastProfileUpdate { get; set; }

        // Time Statistics
        public TimeSpentDto TimeSpent { get; set; }

        // Industry Statistics
        public List<IndustryApplicationDto> IndustryApplications { get; set; } = new();

        // Skill Statistics
        public List<SkillFrequencyDto> SkillFrequency { get; set; } = new();
    }

    public class TimeSpentDto
    {
        public int TotalApplicationTimeMinutes { get; set; }
        public int AverageApplicationTimeMinutes { get; set; }
        public int TotalAssessmentTimeMinutes { get; set; }
        public int AverageAssessmentTimeMinutes { get; set; }
        public int TotalProfileTimeMinutes { get; set; }
    }

    public class IndustryApplicationDto
    {
        public string Industry { get; set; }
        public int ApplicationCount { get; set; }
        public int AcceptanceCount { get; set; }
        public decimal AcceptanceRate { get; set; }
    }

    public class SkillFrequencyDto
    {
        public string Skill { get; set; }
        public int UsageCount { get; set; }
        public int SuccessfulApplications { get; set; }
        public decimal SuccessRate { get; set; }
    }
}