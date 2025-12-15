using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Profiles.Queries
{
    public class GetProfileDocumentsQuery : IRequest<List<SupportingDocumentDto>>
    {
        public Guid ProfileId { get; set; }
        public string DocumentType { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? UploadedAfter { get; set; }
        public DateTime? UploadedBefore { get; set; }

        public GetProfileDocumentsQuery(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}