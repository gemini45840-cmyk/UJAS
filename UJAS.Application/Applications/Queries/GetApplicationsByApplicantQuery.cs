using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;

namespace UJAS.Application.Applications.Queries
{
    public class GetApplicationsByApplicantQuery : IRequest<List<ApplicationDto>>
    {
        public Guid ApplicantId { get; set; }
        public bool IncludeWithdrawn { get; set; } = false;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<ApplicationStatusDto> StatusFilter { get; set; } = new();

        public GetApplicationsByApplicantQuery(Guid applicantId)
        {
            ApplicantId = applicantId;
        }
    }
}
