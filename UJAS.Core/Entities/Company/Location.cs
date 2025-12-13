using UJAS.Core.Entities.User;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.Company
{
    public class Location : BaseAuditableEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsHeadquarters { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<CompanyUser> Managers { get; set; } = new List<CompanyUser>();
        public virtual ICollection<RegionalManagerLocation> RegionalManagers { get; set; } = new List<RegionalManagerLocation>();
    }
}