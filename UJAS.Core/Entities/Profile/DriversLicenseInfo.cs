namespace UJAS.Core.Entities.Profile
{
    public class DriversLicenseInfo : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string LicenseNumber { get; set; }
        public string StateProvinceOfIssue { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string LicenseClass { get; set; }
        public string Endorsements { get; set; }
        public string Restrictions { get; set; }
        public string LicenseFrontFilePath { get; set; }
        public string LicenseBackFilePath { get; set; }
        public bool? DrivingRecordConsent { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}

