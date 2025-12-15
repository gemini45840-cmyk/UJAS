using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyWidgetSettingsQuery : IRequest<WidgetSettingsDto>
    {
        public Guid CompanyId { get; set; }
        public bool IncludeEmbedCode { get; set; } = true;

        public GetCompanyWidgetSettingsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

    public class WidgetSettingsDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool EnableWidget { get; set; }
        public string WidgetPosition { get; set; }
        public string WidgetLabel { get; set; }
        public string WidgetColor { get; set; }
        public string WidgetTextColor { get; set; }
        public bool WidgetAutoOpen { get; set; }
        public int WidgetDelaySeconds { get; set; }
        public string WidgetEmbedCode { get; set; }
        public string WidgetJavaScriptUrl { get; set; }
        public string WidgetCSSUrl { get; set; }
        public Dictionary<string, object> CustomWidgetSettings { get; set; } = new();
        public bool IsActive { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}