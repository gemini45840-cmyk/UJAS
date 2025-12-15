using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Fields.Commands
{
    public class ReorderFieldsCommand : IRequest<bool>
    {
        public Guid? CompanyId { get; set; }
        public string Section { get; set; }
        public List<FieldOrderDto> FieldOrders { get; set; } = new();
        public Guid ReorderedBy { get; set; }

        public ReorderFieldsCommand(Guid reorderedBy)
        {
            ReorderedBy = reorderedBy;
        }
    }

    public class FieldOrderDto
    {
        public Guid FieldId { get; set; }
        public int NewOrder { get; set; }
    }
}
