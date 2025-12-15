using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanySettingsCommand : IRequest<bool>
    {
        public Guid CompanyId { get; set; }
        public CompanySettingsUpdateDto Settings { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool ApplyToAllLocations { get; set; } = false;

        public UpdateCompanySettingsCommand(Guid companyId, CompanySettingsUpdateDto settings, Guid updatedBy)
        {
            CompanyId = companyId;
            Settings = settings;
            UpdatedBy = updatedBy;
        }
    }
}