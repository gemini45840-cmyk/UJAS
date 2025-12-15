using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileCompletionQuery : IRequest<ProfileCompletionDto>
    {
        public Guid ProfileId { get; set; }

        public GetProfileCompletionQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }

    public class ProfileCompletionDto
    {
        public Guid ProfileId { get; set; }
        public int OverallCompletion { get; set; }
        public List<SectionCompletionDto> Sections { get; set; } = new();
        public List<string> MissingRequiredFields { get; set; } = new();
        public DateTime? LastCompletionCheck { get; set; }
        public bool IsProfileReadyForApplications { get; set; }
        public int DaysSinceLastUpdate { get; set; }
    }

    public class SectionCompletionDto
    {
        public string SectionName { get; set; }
        public int CompletionPercentage { get; set; }
        public bool IsRequired { get; set; }
        public int RequiredFieldsCount { get; set; }
        public int CompletedFieldsCount { get; set; }
        public List<string> MissingFields { get; set; } = new();
    }
}
