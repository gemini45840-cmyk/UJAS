using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Commands
{
    public class MergeDuplicateProfilesCommand : IRequest<Guid>
    {
        public List<Guid> ProfileIds { get; set; } = new();
        public Guid PrimaryProfileId { get; set; }
        public MergeOptionsDto MergeOptions { get; set; }
        public Guid MergedBy { get; set; }

        public MergeDuplicateProfilesCommand(Guid primaryProfileId, Guid mergedBy)
        {
            PrimaryProfileId = primaryProfileId;
            MergedBy = mergedBy;
        }
    }

    public class MergeOptionsDto
    {
        public bool MergePersonalInfo { get; set; } = true;
        public bool MergeEducation { get; set; } = true;
        public bool MergeExperience { get; set; } = true;
        public bool MergeSkills { get; set; } = true;
        public bool MergeDocuments { get; set; } = true;
        public ConflictResolutionStrategy ConflictResolution { get; set; } = ConflictResolutionStrategy.KeepNewest;
        public bool DeleteMergedProfiles { get; set; } = true;
        public bool SendNotification { get; set; } = true;
    }

    public enum ConflictResolutionStrategy
    {
        KeepNewest,
        KeepOldest,
        KeepPrimary,
        ManualReview
    }
}
