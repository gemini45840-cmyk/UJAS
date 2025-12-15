using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest<bool>
    {
        public CompanyUpdateDto Company { get; set; }
        public Guid UpdatedBy { get; set; }
        public string UpdateReason { get; set; }

        public UpdateCompanyCommand(CompanyUpdateDto company, Guid updatedBy)
        {
            Company = company;
            UpdatedBy = updatedBy;
        }
    }
}
