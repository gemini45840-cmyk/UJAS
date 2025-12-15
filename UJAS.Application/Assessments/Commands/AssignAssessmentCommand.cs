using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Commands
{
    public class AssignAssessmentCommand : IRequest<bool>
    {
        public Guid AssessmentId { get; set; }
        public List<Guid> PositionIds { get; set; } = new();
        public List<Guid> LocationIds { get; set; } = new();
        public List<string> JobTitles { get; set; } = new();
        public List<string> Departments { get; set; } = new();
        public AssignmentTypeDto AssignmentType { get; set; }
        public string AssignmentRule { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid AssignedBy { get; set; }

        public AssignAssessmentCommand(Guid assessmentId, Guid assignedBy)
        {
            AssessmentId = assessmentId;
            AssignedBy = assignedBy;
        }
    }
}