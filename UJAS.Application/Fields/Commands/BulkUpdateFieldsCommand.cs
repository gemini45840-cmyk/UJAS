using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Fields.DTOs;

namespace UJAS.Application.Fields.Commands
{
    public class BulkUpdateFieldsCommand : IRequest<BulkUpdateResult>
    {
        public List<Guid> FieldIds { get; set; } = new();
        public FieldBulkUpdateDto Updates { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdateReason { get; set; }

        public BulkUpdateFieldsCommand(FieldBulkUpdateDto updates, Guid updatedBy)
        {
            Updates = updates;
            UpdatedBy = updatedBy;
        }
    }

    public class FieldBulkUpdateDto
    {
        public bool? IsRequired { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVisible { get; set; }
        public string Section { get; set; }
        public PrivacyLevelDto? PrivacyLevel { get; set; }
        public int? OrderOffset { get; set; }
        public Dictionary<string, object> CustomUpdates { get; set; } = new();
    }

    public class BulkUpdateResult
    {
        public int TotalFields { get; set; }
        public int UpdatedFields { get; set; }
        public int FailedFields { get; set; }
        public List<FieldUpdateError> Errors { get; set; } = new();
        public DateTime CompletedAt { get; set; }
    }

    public class FieldUpdateError
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
    }
}