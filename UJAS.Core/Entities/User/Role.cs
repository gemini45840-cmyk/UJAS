using UJAS.Core.Enums;

namespace UJAS.Core.Entities.User
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}