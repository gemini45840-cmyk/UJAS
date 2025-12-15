using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class ApplicationCommentDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public Guid CommentedById { get; set; }
        public string CommentedByName { get; set; }
        public string CommentedByRole { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsVisibleToApplicant { get; set; }
        public bool IsInternalNote { get; set; }
    }
}
