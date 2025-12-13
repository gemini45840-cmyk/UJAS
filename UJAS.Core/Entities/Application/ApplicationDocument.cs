using UJAS.Core.Enums;

namespace UJAS.Core.Entities.Application
{
    public class ApplicationDocument : BaseEntity
    {
        public int ApplicationId { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public DateTime UploadDate { get; set; }

        // Navigation properties
        public virtual Application Application { get; set; }
    }
}