using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Assessments.Commands
{
    public class DuplicateAssessmentCommand : IRequest<Guid>
    {
        public Guid AssessmentId { get; set; }
        public string NewName { get; set; }
        public Guid CompanyId { get; set; }
        public Guid DuplicatedBy { get; set; }
        public bool CopyQuestions { get; set; } = true;
        public bool CopySettings { get; set; } = true;
        public bool CopyAssignments { get; set; } = false;

        public DuplicateAssessmentCommand(Guid assessmentId, string newName, Guid companyId, Guid duplicatedBy)
        {
            AssessmentId = assessmentId;
            NewName = newName;
            CompanyId = companyId;
            DuplicatedBy = duplicatedBy;
        }
    }
}