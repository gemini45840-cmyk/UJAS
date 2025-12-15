using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Companies.DTOs;

namespace UJAS.Application.Companies.Queries
{
    public class GetLocationByIdQuery : IRequest<LocationDto>
    {
        public Guid LocationId { get; set; }
        public bool IncludeManagers { get; set; } = true;
        public bool IncludeStatistics { get; set; } = true;
        public Guid? UserId { get; set; }
        public string UserRole { get; set; }

        public GetLocationByIdQuery(Guid locationId)
        {
            LocationId = locationId;
        }
    }
}