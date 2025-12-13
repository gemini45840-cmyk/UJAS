namespace UJAS.Core.Entities.User
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
    }
}