namespace UJAS.Core.Entities.Field
{
    public class LocationField : BaseEntity
    {
        public int CompanyFieldId { get; set; }
        public int LocationId { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsRequired { get; set; }
        public string CustomLabel { get; set; }
        public string CustomHelpText { get; set; }

        // Navigation properties
        public virtual CompanyField CompanyField { get; set; }
        public virtual Location Location { get; set; }
    }
}