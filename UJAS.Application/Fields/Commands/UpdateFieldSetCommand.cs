using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class UpdateFieldSetCommand : IRequest<bool>
    {
        public Guid FieldSetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool? IsActive { get; set; }
        public int? Order { get; set; }
        public FieldSetSettingsUpdateDto Settings { get; set; }
        public List<Guid> AddFieldIds { get; set; } = new();
        public List<Guid> RemoveFieldIds { get; set; } = new();
        public Guid UpdatedBy { get; set; }

        public UpdateFieldSetCommand(Guid fieldSetId, Guid updatedBy)
        {
            FieldSetId = fieldSetId;
            UpdatedBy = updatedBy;
        }
    }

    public class FieldSetSettingsUpdateDto
    {
        public bool? ShowTitle { get; set; }
        public bool? Collapsible { get; set; }
        public bool? CollapsedByDefault { get; set; }
        public bool? AllowReordering { get; set; }
        public bool? ShowProgress { get; set; }
        public string Layout { get; set; }
        public int? Columns { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
    }
}
