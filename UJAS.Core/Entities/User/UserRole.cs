using UJAS.Core.Entities.Company;

namespace UJAS.Core.Entities.User
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }

        // Navigation properties
        public virtual tUser User { get; set; }
        public virtual Role Role { get; set; }
        public virtual tCompany Company { get; set; }
    }
}
