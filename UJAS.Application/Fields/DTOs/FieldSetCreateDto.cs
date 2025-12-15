using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldSetCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Guid? CompanyId { get; set; }
        public bool IsSystemSet { get; set; } = false;
        public int Order { get; set; }

        // Fields in the set
        public List<Guid> FieldIds { get; set; } = new();
        public List<FieldCreateDto> NewFields { get; set; } = new();

        // Settings
        public FieldSetSettingsCreateDto Settings { get; set; }

        // Template
        public Guid? TemplateId { get; set; }
    }

    public class FieldSetSettingsCreateDto
    {
        public bool ShowTitle { get; set; } = true;
        public bool Collapsible { get; set; } = false;
        public bool CollapsedByDefault { get; set; } = false;
        public bool AllowReordering { get; set; } = true;
        public bool ShowProgress { get; set; } = true;
        public string Layout { get; set; } = "Vertical";
        public int Columns { get; set; } = 1;
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
    }
}