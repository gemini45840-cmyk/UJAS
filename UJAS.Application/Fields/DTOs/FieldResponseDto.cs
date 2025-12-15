using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.DTOs
{
    public class FieldResponseDto
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public FieldTypeDto FieldType { get; set; }
        public string Value { get; set; }
        public List<string> MultiSelectValues { get; set; } = new();
        public Dictionary<string, string> FileValues { get; set; } = new();
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; } = new();
        public DateTime? ResponseDate { get; set; }
        public Guid? RespondedBy { get; set; }
        public string RespondedByName { get; set; }
    }

    public class FieldValidationResultDto
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public bool IsValid { get; set; }
        public List<ValidationErrorDto> Errors { get; set; } = new();
        public List<ValidationWarningDto> Warnings { get; set; } = new();
        public List<ValidationSuggestionDto> Suggestions { get; set; } = new();
        public DateTime ValidatedAt { get; set; }
    }

    public class ValidationErrorDto
    {
        public string Rule { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; } // Error, Warning, Info
        public string FieldPath { get; set; }
        public string SuggestedValue { get; set; }
    }

    public class ValidationWarningDto
    {
        public string WarningType { get; set; }
        public string Message { get; set; }
        public string Suggestion { get; set; }
    }

    public class ValidationSuggestionDto
    {
        public string SuggestionType { get; set; }
        public string Message { get; set; }
        public string Benefit { get; set; }
    }

    public class FieldValueAnalysisDto
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public int TotalResponses { get; set; }
        public int ValidResponses { get; set; }
        public int InvalidResponses { get; set; }
        public decimal ValidityRate { get; set; }
        public decimal CompletionRate { get; set; }
        public List<CommonValueDto> CommonValues { get; set; } = new();
        public List<ValidationPatternDto> CommonErrors { get; set; } = new();
        public Dictionary<string, int> OptionDistribution { get; set; } = new();
        public List<TrendDataDto> TrendData { get; set; } = new();
    }

    public class CommonValueDto
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
        public bool IsValid { get; set; }
    }

    public class ValidationPatternDto
    {
        public string ErrorType { get; set; }
        public string Pattern { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
        public string CommonExample { get; set; }
    }

    public class TrendDataDto
    {
        public DateTime Date { get; set; }
        public int TotalResponses { get; set; }
        public int ValidResponses { get; set; }
        public decimal ValidityRate { get; set; }
    }
}
