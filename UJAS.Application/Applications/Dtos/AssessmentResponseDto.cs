using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class AssessmentResponseDto
    {
        public Guid AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; } // Personality, Skills, Cognitive, etc.
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Status { get; set; } // NotStarted, InProgress, Completed, Expired
        public int? Score { get; set; }
        public string Result { get; set; }
        public List<QuestionResponseDto> QuestionResponses { get; set; } = new();
        public string AssessmentUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int TimeLimitMinutes { get; set; }
    }

    public class QuestionResponseDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Response { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}