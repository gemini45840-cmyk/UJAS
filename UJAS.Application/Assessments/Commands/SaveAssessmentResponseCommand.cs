using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Assessments.DTOs;

namespace UJAS.Application.Assessments.Commands
{
    public class SaveAssessmentResponseCommand : IRequest<bool>
    {
        public Guid ResponseId { get; set; }
        public List<QuestionResponseUpdateDto> Responses { get; set; } = new();
        public int TimeSpentSeconds { get; set; }
        public bool IsFinalSubmission { get; set; } = false;
        public List<ProctoringEventDto> ProctoringEvents { get; set; } = new();

        public SaveAssessmentResponseCommand(Guid responseId)
        {
            ResponseId = responseId;
        }
    }

    public class QuestionResponseUpdateDto
    {
        public Guid QuestionId { get; set; }
        public string Response { get; set; }
        public List<string> MultiSelectResponses { get; set; } = new();
        public Dictionary<string, string> FileResponses { get; set; } = new();
        public string FileUrl { get; set; }
        public string VideoUrl { get; set; }
        public string AudioUrl { get; set; }
        public int? TimeTakenSeconds { get; set; }
    }
}
