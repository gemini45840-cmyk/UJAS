using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class DocumentsDto
    {
        // Resume/CV
        public string ResumeFileName { get; set; }
        public string ResumeFileUrl { get; set; }
        public string ResumeFileType { get; set; }
        public long? ResumeFileSize { get; set; }
        public DateTime? ResumeLastUpdated { get; set; }
        public string ResumeVisibility { get; set; } // Visible to all, Hidden, Specific companies

        // Cover Letter
        public string CoverLetterFileName { get; set; }
        public string CoverLetterFileUrl { get; set; }
        public string CoverLetterText { get; set; }
        public bool UseCustomCoverLetter { get; set; }

        // Supporting Documents
        public List<SupportingDocumentDto> SupportingDocuments { get; set; } = new();

        // Photo
        public string PhotoFileName { get; set; }
        public string PhotoFileUrl { get; set; }
        public bool PhotoUsageConsent { get; set; }
    }

    public class SupportingDocumentDto
    {
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
    }
}