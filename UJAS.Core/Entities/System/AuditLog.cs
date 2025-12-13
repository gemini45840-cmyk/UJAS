namespace UJAS.Core.Entities.System
{
    public class AuditLog : BaseEntity
    {
        public string TableName { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; } // CREATE, UPDATE, DELETE
        public string Changes { get; set; } // JSON of changes
        public string ChangedBy { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
