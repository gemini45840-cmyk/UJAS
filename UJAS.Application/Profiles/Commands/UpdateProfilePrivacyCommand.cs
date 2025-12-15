using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class UpdateProfilePrivacyCommand : IRequest<bool>
    {
        public Guid ProfileId { get; set; }
        public string ProfileVisibility { get; set; }
        public List<Guid> VisibleToCompanies { get; set; }
        public bool? ShowContactInfo { get; set; }
        public bool? ShowSalaryHistory { get; set; }
        public bool? ShowEducationDetails { get; set; }
        public bool? ShowWorkExperience { get; set; }
        public bool? ShowSkills { get; set; }
        public bool? ShowAssessments { get; set; }
        public bool? AllowDataSharing { get; set; }
        public bool? AllowProfileSearch { get; set; }
        public List<Guid> BlockedCompanies { get; set; }
        public Guid UpdatedBy { get; set; }

        public UpdateProfilePrivacyCommand(Guid profileId, Guid updatedBy)
        {
            ProfileId = profileId;
            UpdatedBy = updatedBy;
        }
    }
}
