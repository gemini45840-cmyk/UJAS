using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Commands
{
    public class CreateAssessmentCommand : IRequest<Guid>
    {
        public AssessmentCreateDto Assessment { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CreatedBy { get; set; }
        public bool FromTemplate { get; set; } = false;
        public Guid? TemplateId { get; set; }

        public CreateAssessmentCommand(AssessmentCreateDto assessment, Guid companyId, Guid createdBy)
        {
            Assessment = assessment;
            CompanyId = companyId;
            CreatedBy = createdBy;
        }
    }
}