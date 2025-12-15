using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Queries
{
    public class ValidateProfileQuery : IRequest<ProfileValidationResult>
    {
        public Guid ProfileId { get; set; }
        public bool CheckCompleteness { get; set; } = true;
        public bool CheckConsistency { get; set; } = true;
        public bool CheckDataQuality { get; set; } = true;

        public ValidateProfileQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }

    public class ProfileValidationResult
    {
        public Guid ProfileId { get; set; }
        public bool IsValid { get; set; }
        public int Score { get; set; }
        public List<ValidationIssue> Issues { get; set; } = new();
        public List<ValidationWarning> Warnings { get; set; } = new();
        public List<ValidationRecommendation> Recommendations { get; set; } = new();
        public DateTime ValidationDate { get; set; }
    }

    public class ValidationIssue
    {
        public string Section { get; set; }
        public string Field { get; set; }
        public string IssueType { get; set; } // Missing, Invalid, Inconsistent, Duplicate
        public string Message { get; set; }
        public string Severity { get; set; } // Critical, High, Medium, Low
        public string RecommendedAction { get; set; }
    }

    public class ValidationWarning
    {
        public string Section { get; set; }
        public string Field { get; set; }
        public string WarningType { get; set; }
        public string Message { get; set; }
        public string RecommendedAction { get; set; }
    }

    public class ValidationRecommendation
    {
        public string Area { get; set; }
        public string Recommendation { get; set; }
        public string Benefit { get; set; }
        public string Priority { get; set; } // High, Medium, Low
    }
}
