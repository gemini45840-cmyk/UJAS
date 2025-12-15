using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class CompanyQuestionResponseDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; } // Text, TextArea, Dropdown, Checkbox, etc.
        public string Response { get; set; }
        public List<string> MultiSelectResponses { get; set; } = new();
        public Dictionary<string, string> FileResponses { get; set; } = new(); // For file upload questions
        public bool IsRequired { get; set; }
        public int Order { get; set; }
    }
}
