using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanySubscriptionCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public string Plan { get; set; }
        public string BillingCycle { get; set; }
        public bool AutoRenew { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string Status { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdateReason { get; set; }
        public bool ProRate { get; set; } = true;

        public UpdateCompanySubscriptionCommand(Guid companyId, string plan, Guid updatedBy)
        {
            CompanyId = companyId;
            Plan = plan;
            UpdatedBy = updatedBy;
        }
    }
}
