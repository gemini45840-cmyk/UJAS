using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class EmploymentInformationDto
    {
        // Work Authorization & Eligibility
        public bool IsLegallyAuthorizedToWork { get; set; }
        public bool RequiresVisaSponsorship { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }
        public bool HasFelonyConviction { get; set; }
        public string FelonyExplanation { get; set; }
        public bool WillingBackgroundCheck { get; set; }
        public bool WillingDrugTest { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public string WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public decimal? DesiredSalaryFrom { get; set; }
        public decimal? DesiredSalaryTo { get; set; }
        public bool WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public string NoticePeriodRequired { get; set; }
        public string ReferredByEmployee { get; set; }
        public string HowDidYouHear { get; set; }
    }
}
