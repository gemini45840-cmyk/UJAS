using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UJAS.Core.Entities.User
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Company Company { get; set; }
    }
}
