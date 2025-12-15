using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common.Services;

namespace UJAS.Application.Applications.Dtos
{
    public class StatusHistoryDto
    {
        public Guid Id { get; set; }
        public ApplicationStatusDto OldStatus { get; set; }
        public ApplicationStatusDto NewStatus { get; set; }
        public Guid ChangedById { get; set; }
        public string ChangedByName { get; set; }
        public string ChangeReason { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Notes { get; set; }
    }
}
