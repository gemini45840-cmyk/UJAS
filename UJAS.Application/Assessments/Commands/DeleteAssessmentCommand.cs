using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class DeleteAssessmentCommand : IRequest<bool>
    {
        public Guid AssessmentId { get; set; }
        public Guid DeletedBy { get; set; }
        public string Reason { get; set; }
        public bool ArchiveInstead { get; set; } = true;

        public DeleteAssessmentCommand(Guid assessmentId, Guid deletedBy)
        {
            AssessmentId = assessmentId;
            DeletedBy = deletedBy;
        }
    }
}