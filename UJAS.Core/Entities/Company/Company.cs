using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Assessment;
using UJAS.Core.Entities.Field;
using UJAS.Core.Entities.User;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.Company
{
    public class tCompany : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string WidgetEmbedCode { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? SubscriptionEndDate { get; set; }
        public string TimeZone { get; set; }

        // Navigation properties
        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
        public virtual ICollection<CompanyField> CompanyFields { get; set; } = new List<CompanyField>();
        public virtual ICollection<tAssessment> Assessments { get; set; } = new List<tAssessment>();
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
        public virtual CompanySettings Settings { get; set; }
        public virtual ICollection<tApplication> Applications { get; set; } = new List<tApplication>();
    }
}