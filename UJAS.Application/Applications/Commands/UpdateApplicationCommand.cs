using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;

namespace UJAS.Application.Applications.Commands
{
    public class UpdateApplicationCommand : IRequest<bool>
    {
        public ApplicationUpdateDto Application { get; set; }
        public Guid UpdatedById { get; set; }
        public string UserRole { get; set; }

        public UpdateApplicationCommand(ApplicationUpdateDto application)
        {
            Application = application;
        }
    }
}
