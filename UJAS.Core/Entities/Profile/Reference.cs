namespace UJAS.Core.Entities.Profile
{
    public class Reference : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string CompanyOrganization { get; set; }
        public string Relationship { get; set; }
        public int? YearsKnown { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BestTimeToContact { get; set; }
        public bool? PermissionToContact { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}