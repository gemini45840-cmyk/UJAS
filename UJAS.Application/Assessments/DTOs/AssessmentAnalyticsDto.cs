using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.DTOs
{
    public class AssessmentAnalyticsDto
    {
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public int TotalCompletions { get; set; }
        public int TotalAttempts { get; set; }
        public decimal AverageScore { get; set; }
        public decimal AverageCompletionTimeMinutes { get; set; }
        public decimal PassRate { get; set; }
        public decimal CompletionRate { get; set; }
        public DateTime? FirstCompletionDate { get; set; }
        public DateTime? LastCompletionDate { get; set; }

        // Score Distribution
        public Dictionary<string, int> ScoreDistribution { get; set; } = new(); // Score range -> count

        // Time Distribution
        public Dictionary<string, int> TimeDistribution { get; set; } = new(); // Time range -> count

        // Question Analytics
        public List<QuestionAnalyticsDto> QuestionAnalytics { get; set; } = new();

        // Skill Analytics
        public Dictionary<string, SkillAnalyticsDto> SkillAnalytics { get; set; } = new();

        // Demographic Analytics (if available)
        public Dictionary<string, DemographicAnalyticsDto> DemographicAnalytics { get; set; } = new();

        // Trend Data
        public List<DailyAnalyticsDto> DailyTrends { get; set; } = new();

        // Comparison Data
        public Dictionary<string, ComparisonDataDto> Comparisons { get; set; } = new();
    }

    public class QuestionAnalyticsDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int AttemptCount { get; set; }
        public int CorrectCount { get; set; }
        public decimal CorrectPercentage { get; set; }
        public decimal AverageTimeSeconds { get; set; }
        public decimal DiscriminationIndex { get; set; }
        public decimal DifficultyIndex { get; set; }
        public List<OptionAnalyticsDto> OptionAnalytics { get; set; } = new();
        public List<string> CommonWrongAnswers { get; set; } = new();
        public bool IsFlagged { get; set; }
        public string FlagReason { get; set; }
    }

    public class OptionAnalyticsDto
    {
        public string OptionText { get; set; }
        public string OptionValue { get; set; }
        public int SelectionCount { get; set; }
        public decimal SelectionPercentage { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class SkillAnalyticsDto
    {
        public string SkillName { get; set; }
        public decimal AverageScore { get; set; }
        public decimal MaxScore { get; set; }
        public decimal MinScore { get; set; }
        public int TestedCount { get; set; }
        public List<Guid> QuestionIds { get; set; } = new();
    }

    public class DemographicAnalyticsDto
    {
        public string DemographicGroup { get; set; }
        public int Count { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public decimal AverageTimeMinutes { get; set; }
    }

    public class DailyAnalyticsDto
    {
        public DateTime Date { get; set; }
        public int Completions { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public int NewAttempts { get; set; }
    }

    public class ComparisonDataDto
    {
        public string ComparisonGroup { get; set; } // e.g., "Location", "Position", "Department"
        public Dictionary<string, GroupAnalyticsDto> Groups { get; set; } = new();
    }

    public class GroupAnalyticsDto
    {
        public string GroupName { get; set; }
        public int Completions { get; set; }
        public decimal AverageScore { get; set; }
        public decimal PassRate { get; set; }
        public decimal AverageTimeMinutes { get; set; }
    }
}
