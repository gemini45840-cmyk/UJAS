using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanyCustomFieldCommand : IRequest<bool>
    {
        public Guid CustomFieldId { get; set; }
        public string DisplayName { get; set; }
        public bool? IsRequired { get; set; }
        public int? Order { get; set; }
        public string Section { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }
        public string DefaultValue { get; set; }
        public string ValidationRules { get; set; }
        public List<string> Options { get; set; }
        public bool? IsVisibleToApplicant { get; set; }
        public bool? IsVisibleToManagers { get; set; }
        public bool? IsVisibleToAdmins { get; set; }
        public bool? IsSearchable { get; set; }
        public bool? IsFilterable { get; set; }
        public bool? IsActive { get; set; }
        public string PrivacyLevel { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateCompanyCustomFieldCommand(Guid customFieldId, Guid updatedBy)
        {
            CustomFieldId = customFieldId;
            UpdatedBy = updatedBy;
        }
    }
}