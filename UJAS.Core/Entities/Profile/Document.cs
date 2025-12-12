using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Enums;
using UJAS.Core.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace UJAS.Core.Entities.Profile
{
    public class Document : BaseEntity
    {
        public int ApplicantProfileId { get; set; }
        public virtual ApplicantProfile ApplicantProfile { get; set; }

        public DocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(500)]
        public string StoragePath { get; set; }

        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public bool IsParsed { get; set; }
        public string ParsedData { get; set; } // JSON structure of parsed content
        public DateTime? LastUpdated { get; set; }
        public DocumentVisibility Visibility { get; set; } = DocumentVisibility.VisibleToAll;
        public bool IsDefaultForType { get; set; }

        // For specific applications
        public int? ApplicationId { get; set; }
        public virtual Application Application { get; set; }
    }

    public enum DocumentVisibility
    {
        VisibleToAll,
        Hidden,
        SpecificCompaniesOnly
    }
}