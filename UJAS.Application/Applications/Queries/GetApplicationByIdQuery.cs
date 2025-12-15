using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;

namespace UJAS.Application.Applications.Queries
{
    public class GetApplicationByIdQuery : IRequest<ApplicationDetailDto>
    {
        public Guid ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }
        public Guid? CompanyId { get; set; }

        public GetApplicationByIdQuery(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}
