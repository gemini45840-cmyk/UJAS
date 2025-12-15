using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Queries
{
    public class GetUserStatisticsQuery : IRequest<UserStatisticsDto>
    {
        public Guid? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public GetUserStatisticsQuery(Guid? companyId = null)
        {
            CompanyId = companyId;
        }
    }

    public class UserStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int PendingUsers { get; set; }
        public int LockedUsers { get; set; }

        // By user type
        public int SystemAdmins { get; set; }
        public int CompanyAdmins { get; set; }
        public int RegionalManagers { get; set; }
        public int Managers { get; set; }
        public int Applicants { get; set; }

        // Verification stats
        public int EmailVerified { get; set; }
        public int PhoneVerified { get; set; }
        public int TwoFactorEnabled { get; set; }

        // Activity stats
        public int UsersLoggedInToday { get; set; }
        public int UsersLoggedInThisWeek { get; set; }
        public int UsersLoggedInThisMonth { get; set; }
        public int NewUsersToday { get; set; }
        public int NewUsersThisWeek { get; set; }
        public int NewUsersThisMonth { get; set; }

        // Geographic stats
        public Dictionary<string, int> UsersByCountry { get; set; } = new();
        public Dictionary<string, int> UsersByTimeZone { get; set; } = new();

        // Device stats
        public Dictionary<string, int> UsersByDeviceType { get; set; } = new();
        public Dictionary<string, int> UsersByBrowser { get; set; } = new();

        // Trend data
        public List<DailyUserCountDto> DailyNewUsers { get; set; } = new();
        public List<DailyUserCountDto> DailyActiveUsers { get; set; } = new();
    }

    public class DailyUserCountDto
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}