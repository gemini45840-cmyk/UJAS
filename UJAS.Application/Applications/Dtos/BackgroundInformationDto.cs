using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class BackgroundInformationDto
    {
        // Driver's License
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseState { get; set; }
        public DateTime? DriversLicenseExpiration { get; set; }
        public string LicenseClass { get; set; }
        public string Endorsements { get; set; }
        public string Restrictions { get; set; }
        public bool DrivingRecordConsent { get; set; }

        // Criminal History
        public bool HasCriminalConviction { get; set; }
        public string CriminalConvictionDetails { get; set; }
        public bool HasPendingCharges { get; set; }
        public string PendingChargesDetails { get; set; }

        // Professional Licenses & Certifications
        public List<LicenseCertificationDto> LicensesCertifications { get; set; } = new();
    }

    public class LicenseCertificationDto
    {
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public string LicenseNumber { get; set; }
        public string StateCountryOfIssue { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string FileUrl { get; set; }
    }
}
