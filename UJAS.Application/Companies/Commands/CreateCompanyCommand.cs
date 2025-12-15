using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<Guid>
    {
        public CompanyCreateDto Company { get; set; }
        public Guid CreatedBy { get; set; }
        public bool SendWelcomeEmail { get; set; } = true;
        public bool ActivateImmediately { get; set; } = true;

        public CreateCompanyCommand(CompanyCreateDto company, Guid createdBy)
        {
            Company = company;
            CreatedBy = createdBy;
        }
    }
}
