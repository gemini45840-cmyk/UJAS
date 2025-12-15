using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } // Internal location code
        public string Type { get; set; } // Headquarters, Branch, Store, Warehouse, Remote
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPrimary { get; set; } = false;

        // Address
        public AddressDto Address { get; set; }

        // Business Hours
        public BusinessHoursDto BusinessHours { get; set; }

        // Location Settings
        public LocationSettingsDto Settings { get; set; }

        // Managers
        public List<LocationManagerDto> Managers { get; set; } = new();

        // Regional Managers (who oversee multiple locations)
        public List<LocationRegionalManagerDto> RegionalManagers { get; set; } = new();

        // Statistics
        public LocationStatisticsDto Statistics { get; set; }

        // Metadata
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }

    public class BusinessHoursDto
    {
        public List<DayHoursDto> RegularHours { get; set; } = new();
        public List<HolidayDto> Holidays { get; set; } = new();
        public string TimeZone { get; set; }
        public bool Is24Hours { get; set; } = false;
        public bool IsOpenOnWeekends { get; set; } = false;
    }

    public class DayHoursDto
    {
        public string Day { get; set; } // Monday, Tuesday, etc.
        public bool IsOpen { get; set; } = true;
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public bool Is24Hours { get; set; } = false;
    }

    public class HolidayDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; } = true;
        public string Description { get; set; }
    }

    public class LocationSettingsDto
    {
        public bool AcceptApplications { get; set; } = true;
        public List<string> AcceptedPositionTypes { get; set; } = new();
        public bool RequireLocationSpecificQuestions { get; set; } = false;
        public List<Guid> RequiredAssessments { get; set; } = new();
        public bool NotifyManagersOnNewApplication { get; set; } = true;
        public List<string> NotificationEmails { get; set; } = new();
        public string ApplicationInstructions { get; set; }
        public string InterviewInstructions { get; set; }
        public bool EnableGeofencing { get; set; } = false;
        public decimal? GeofenceRadiusMiles { get; set; } = 25;
        public int MaxApplicationsPerDay { get; set; } = 100;
    }

    public class LocationManagerDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public List<string> Permissions { get; set; } = new();
        public bool IsPrimary { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

    public class LocationRegionalManagerDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public List<Guid> ManagedLocationIds { get; set; } = new();
        public DateTime AssignedDate { get; set; }
        public Guid AssignedBy { get; set; }
        public string AssignedByName { get; set; }
        public List<string> Permissions { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }

    public class LocationStatisticsDto
    {
        public int TotalApplications { get; set; }
        public int ActiveApplications { get; set; }
        public int TotalPositions { get; set; }
        public int OpenPositions { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalManagers { get; set; }
        public decimal ApplicationCompletionRate { get; set; }
        public decimal AverageApplicationTimeMinutes { get; set; }
        public DateTime? LastApplicationDate { get; set; }
    }
}