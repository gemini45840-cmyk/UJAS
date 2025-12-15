using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Users.Commands
{
    public class GenerateApiKeyCommand : IRequest<string>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public List<string> Permissions { get; set; } = new();
        public string Description { get; set; }
        public Guid GeneratedBy { get; set; }

        public GenerateApiKeyCommand(Guid userId, string name, Guid generatedBy)
        {
            UserId = userId;
            Name = name;
            GeneratedBy = generatedBy;
        }
    }
}
