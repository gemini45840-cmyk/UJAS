using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class ReferenceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Relationship { get; set; }
        public int YearsKnown { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BestTimeToContact { get; set; }
        public bool PermissionToContact { get; set; }
    }
}
