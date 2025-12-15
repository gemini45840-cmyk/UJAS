using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateLocationCommand : IRequest<bool>
    {
        public Guid LocationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPrimary { get; set; }
        public AddressUpdateDto Address { get; set; }
        public BusinessHoursUpdateDto BusinessHours { get; set; }
        public LocationSettingsUpdateDto Settings { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateLocationCommand(Guid locationId, Guid updatedBy)
        {
            LocationId = locationId;
            UpdatedBy = updatedBy;
        }
    }

    public class BusinessHoursUpdateDto
    {
        public List<DayHoursUpdateDto> RegularHours { get; set; }
        public List<HolidayUpdateDto> Holidays { get; set; }
        public string TimeZone { get; set; }
        public bool? Is24Hours { get; set; }
        public bool? IsOpenOnWeekends { get; set; }
    }

    public class DayHoursUpdateDto
    {
        public string Day { get; set; }
        public bool? IsOpen { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public bool? Is24Hours { get; set; }
    }

    public class HolidayUpdateDto
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsRecurring { get; set; }
        public string Description { get; set; }
    }

    public class LocationSettingsUpdateDto
    {
        public bool? AcceptApplications { get; set; }
        public List<string> AcceptedPositionTypes { get; set; }
        public bool? RequireLocationSpecificQuestions { get; set; }
        public List<Guid> RequiredAssessments { get; set; }
        public bool? NotifyManagersOnNewApplication { get; set; }
        public List<string> NotificationEmails { get; set; }
        public string ApplicationInstructions { get; set; }
        public string InterviewInstructions { get; set; }
        public bool? EnableGeofencing { get; set; }
        public decimal? GeofenceRadiusMiles { get; set; }
        public int? MaxApplicationsPerDay { get; set; }
    }
}
