namespace UJAS.Application.Common.DTOs
{
    public class FileAttachmentDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string FileData { get; set; } // Base64 encoded
        public string FileUrl { get; set; }
    }
}