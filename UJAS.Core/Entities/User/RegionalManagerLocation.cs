using UJAS.Core.Entities.Company;

namespace UJAS.Core.Entities.User
{
    public class RegionalManagerLocation : BaseEntity
    {
        public int CompanyUserId { get; set; }
        public int LocationId { get; set; }

        // Navigation properties
        public virtual CompanyUser CompanyUser { get; set; }
        public virtual Location Location { get; set; }
    }
}
