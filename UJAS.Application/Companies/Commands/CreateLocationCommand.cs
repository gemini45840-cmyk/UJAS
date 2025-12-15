using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Commands
{
    public class CreateLocationCommand : IRequest<Guid>
    {
        public LocationCreateDto Location { get; set; }
        public Guid CreatedBy { get; set; }
        public bool ActivateImmediately { get; set; } = true;

        public CreateLocationCommand(LocationCreateDto location, Guid createdBy)
        {
            Location = location;
            CreatedBy = createdBy;
        }
    }
}  