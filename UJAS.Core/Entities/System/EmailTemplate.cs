namespace UJAS.Core.Entities.System
{
    public class EmailTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
        public string Variables { get; set; } // JSON of available variables
        public string Category { get; set; }
        public bool IsSystemTemplate { get; set; } = true;
        public int? CompanyId { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
    }
}