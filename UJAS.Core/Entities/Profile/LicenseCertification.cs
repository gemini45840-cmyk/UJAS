namespace UJAS.Core.Entities.Profile
{
    public class LicenseCertification : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string Name { get; set; }
        public string IssuingOrganization { get; set; }
        public string LicenseNumber { get; set; }
        public string StateCountryOfIssue { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string FilePath { get; set; }
        public DateTime? RenewalDate { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}