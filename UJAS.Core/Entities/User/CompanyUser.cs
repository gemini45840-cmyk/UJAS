using UJAS.Core.Entities.Company;

namespace UJAS.Core.Entities.User
{
    public class CompanyUser : BaseEntity
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int? LocationId { get; set; }
        public bool IsCompanyAdmin { get; set; }
        public bool IsRegionalManager { get; set; }
        public bool IsManager { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }

        // Navigation properties
        public virtual tUser User { get; set; }
        public virtual tCompany Company { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<RegionalManagerLocation> RegionalManagerLocations { get; set; } = new List<RegionalManagerLocation>();
    }
}
