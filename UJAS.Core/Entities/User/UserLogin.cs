namespace UJAS.Core.Entities.User
{
    public class UserLogin : BaseEntity
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }

        // Navigation properties
        public virtual tUser User { get; set; }
    }
}