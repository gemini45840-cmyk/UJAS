namespace UJAS.Core.Entities.User
{
    public class UserClaim : BaseEntity
    {
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
    }
}
