using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class UploadProfileDocumentCommand : IRequest<Guid>
    {
        public Guid ProfileId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FileContentBase64 { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public Guid UploadedBy { get; set; }
        public bool ParseContent { get; set; } = true; // Parse resume for data extraction

        public UploadProfileDocumentCommand(Guid profileId, string documentType, string fileName, Guid uploadedBy)
        {
            ProfileId = profileId;
            DocumentType = documentType;
            FileName = fileName;
            UploadedBy = uploadedBy;
        }
    }
}