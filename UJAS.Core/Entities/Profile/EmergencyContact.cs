namespace UJAS.Core.Entities.Profile
{
    public class EmergencyContact : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}