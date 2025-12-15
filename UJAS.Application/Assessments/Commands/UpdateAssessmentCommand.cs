using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Commands
{
    public class UpdateAssessmentCommand : IRequest<bool>
    {
        public AssessmentUpdateDto Assessment { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool CreateNewVersion { get; set; } = false;
        public string ChangeNotes { get; set; }

        public UpdateAssessmentCommand(AssessmentUpdateDto assessment, Guid updatedBy)
        {
            Assessment = assessment;
            UpdatedBy = updatedBy;
        }
    }
}
