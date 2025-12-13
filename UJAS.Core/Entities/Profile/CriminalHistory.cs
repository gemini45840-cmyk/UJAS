namespace UJAS.Core.Entities.Profile
{
    public class CriminalHistory : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public bool? HasCriminalConviction { get; set; }
        public string ConvictionDetails { get; set; }
        public bool? HasPendingCharges { get; set; }
        public string PendingChargesDetails { get; set; }

        // Navigation properties
        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}
