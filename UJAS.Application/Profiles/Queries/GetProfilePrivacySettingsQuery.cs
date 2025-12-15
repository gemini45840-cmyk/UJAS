using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfilePrivacySettingsQuery : IRequest<ProfilePrivacyDto>
    {
        public Guid ProfileId { get; set; }

        public GetProfilePrivacySettingsQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }

    public class ProfilePrivacyDto
    {
        public Guid ProfileId { get; set; }
        public string ProfileVisibility { get; set; }
        public List<Guid> VisibleToCompanies { get; set; } = new();
        public bool ShowContactInfo { get; set; }
        public bool ShowSalaryHistory { get; set; }
        public bool ShowEducationDetails { get; set; }
        public bool ShowWorkExperience { get; set; }
        public bool ShowSkills { get; set; }
        public bool ShowAssessments { get; set; }
        public bool AllowDataSharing { get; set; }
        public bool AllowProfileSearch { get; set; }
        public List<Guid> BlockedCompanies { get; set; } = new();
        public DateTime? LastPrivacyUpdate { get; set; }
    }
}