using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanyBrandingCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public BrandingUpdateDto Branding { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateCompanyBrandingCommand(Guid companyId, BrandingUpdateDto branding, Guid updatedBy)
        {
            CompanyId = companyId;
            Branding = branding;
            UpdatedBy = updatedBy;
        }
    }
}