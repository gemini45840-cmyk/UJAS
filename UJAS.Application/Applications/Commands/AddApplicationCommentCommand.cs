using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Commands
{
    public class AddApplicationCommentCommand : IRequest<Guid>
    {
        public Guid ApplicationId { get; set; }
        public string Comment { get; set; }
        public Guid CommentedById { get; set; }
        public string CommentedByName { get; set; }
        public string CommentedByRole { get; set; }
        public bool IsVisibleToApplicant { get; set; } = true;
        public bool IsInternalNote { get; set; } = false;

        public AddApplicationCommentCommand(Guid applicationId, string comment, Guid commentedById)
        {
            ApplicationId = applicationId;
            Comment = comment;
            CommentedById = commentedById;
        }
    }
}