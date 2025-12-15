using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class AddCompanyCustomFieldCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string FieldType { get; set; }
        public string DataType { get; set; }
        public bool IsRequired { get; set; } = false;
        public int Order { get; set; }
        public string Section { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }
        public string DefaultValue { get; set; }
        public string ValidationRules { get; set; }
        public List<string> Options { get; set; } = new();
        public bool IsVisibleToApplicant { get; set; } = true;
        public bool IsVisibleToManagers { get; set; } = true;
        public bool IsVisibleToAdmins { get; set; } = true;
        public bool IsSearchable { get; set; } = false;
        public bool IsFilterable { get; set; } = false;
        public string PrivacyLevel { get; set; } = "Internal";
        public Guid CreatedBy { get; set; }

        public AddCompanyCustomFieldCommand(Guid companyId, string fieldName, string displayName, Guid createdBy)
        {
            CompanyId = companyId;
            FieldName = fieldName;
            DisplayName = displayName;
            CreatedBy = createdBy;
        }
    }
}