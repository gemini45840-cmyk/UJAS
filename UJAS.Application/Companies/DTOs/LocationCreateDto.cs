using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.DTOs
{
    public class LocationCreateDto
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; } = "Branch";
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPrimary { get; set; } = false;

        // Address
        public AddressCreateDto Address { get; set; }

        // Business Hours
        public BusinessHoursCreateDto BusinessHours { get; set; }

        // Settings
        public LocationSettingsCreateDto Settings { get; set; }

        // Initial Manager
        public LocationManagerCreateDto InitialManager { get; set; }
    }

    public class BusinessHoursCreateDto
    {
        public List<DayHoursCreateDto> RegularHours { get; set; } = new();
        public List<HolidayCreateDto> Holidays { get; set; } = new();
        public string TimeZone { get; set; } = "America/New_York";
        public bool Is24Hours { get; set; } = false;
        public bool IsOpenOnWeekends { get; set; } = false;
    }

    public class DayHoursCreateDto
    {
        public string Day { get; set; }
        public bool IsOpen { get; set; } = true;
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public bool Is24Hours { get; set; } = false;
    }

    public class HolidayCreateDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; } = true;
        public string Description { get; set; }
    }

    public class LocationSettingsCreateDto
    {
        public bool AcceptApplications { get; set; } = true;
        public List<string> AcceptedPositionTypes { get; set; } = new();
        public bool RequireLocationSpecificQuestions { get; set; } = false;
        public bool NotifyManagersOnNewApplication { get; set; } = true;
        public List<string> NotificationEmails { get; set; } = new();
        public string ApplicationInstructions { get; set; }
        public int MaxApplicationsPerDay { get; set; } = 100;
    }

    public class LocationManagerCreateDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsPrimary { get; set; } = true;
        public List<string> Permissions { get; set; } = new()
        {
            "ViewApplications",
            "UpdateApplicationStatus",
            "AddComments",
            "ViewAnalytics"
        };
    }
}