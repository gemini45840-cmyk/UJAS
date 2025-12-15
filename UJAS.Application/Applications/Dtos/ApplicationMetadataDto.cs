using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationMetadataDto
    {
        public string ApplicationId { get; set; }
        public string IpAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string DeviceInfo { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignTrackingCode { get; set; }
        public int CompletionTimeMinutes { get; set; }
        public string ApplicationVersion { get; set; }
        public bool IsComplete { get; set; }
        public int CompletionPercentage { get; set; }
        public DateTime? SubmittedDate { get; set; }
    }
}
