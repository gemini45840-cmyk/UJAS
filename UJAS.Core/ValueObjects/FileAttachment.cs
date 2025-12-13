namespace UJAS.Core.ValueObjects
{
    public class FileAttachment
    {
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string ContentType { get; private set; }
        public long FileSize { get; private set; }
        public DateTime UploadDate { get; private set; }
        public string UploadedBy { get; private set; }

        private FileAttachment() { }

        public FileAttachment(string fileName, string filePath, string contentType,
                            long fileSize, string uploadedBy)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name is required");
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is required");
            if (fileSize <= 0)
                throw new ArgumentException("File size must be positive");

            FileName = fileName.Trim();
            FilePath = filePath.Trim();
            ContentType = contentType?.Trim() ?? "application/octet-stream";
            FileSize = fileSize;
            UploadDate = DateTime.UtcNow;
            UploadedBy = uploadedBy ?? "system";
        }

        public string GetFileSizeDisplay()
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = FileSize;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}