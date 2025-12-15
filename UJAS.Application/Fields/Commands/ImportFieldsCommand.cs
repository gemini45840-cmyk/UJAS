using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class ImportFieldsCommand : IRequest<ImportResult>
    {
        public Guid? CompanyId { get; set; }
        public string ImportFormat { get; set; } = "JSON";
        public byte[] ImportData { get; set; }
        public ImportOptionsDto Options { get; set; }
        public Guid ImportedBy { get; set; }

        public ImportFieldsCommand(Guid? companyId, byte[] importData, Guid importedBy)
        {
            CompanyId = companyId;
            ImportData = importData;
            ImportedBy = importedBy;
        }
    }

    public class ImportOptionsDto
    {
        public bool OverrideExisting { get; set; } = false;
        public bool SkipDuplicates { get; set; } = true;
        public bool ValidateBeforeImport { get; set; } = true;
        public bool ImportFieldSets { get; set; } = true;
        public bool ImportTemplates { get; set; } = false;
        public bool DryRun { get; set; } = false;
    }

    public class ImportResult
    {
        public int TotalRecords { get; set; }
        public int ImportedFields { get; set; }
        public int ImportedFieldSets { get; set; }
        public int ImportedTemplates { get; set; }
        public int SkippedRecords { get; set; }
        public int FailedRecords { get; set; }
        public List<ImportError> Errors { get; set; } = new();
        public List<ImportWarning> Warnings { get; set; } = new();
        public DateTime ImportedAt { get; set; }
    }

    public class ImportError
    {
        public int LineNumber { get; set; }
        public string RecordType { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
    }

    public class ImportWarning
    {
        public int LineNumber { get; set; }
        public string RecordType { get; set; }
        public string Warning { get; set; }
        public string Details { get; set; }
    }
}
