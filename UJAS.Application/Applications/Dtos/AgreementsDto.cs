using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Applications.Dtos
{
    public class AgreementsDto
    {
        // Legal Agreements
        public bool ApplicationTruthfulness { get; set; }
        public bool AtWillEmploymentAcknowledgment { get; set; }
        public bool NonCompeteAcknowledgment { get; set; }
        public bool BackgroundCheckAuthorization { get; set; }
        public bool DrugTestingConsent { get; set; }
        public bool EmploymentVerificationAuthorization { get; set; }
        public bool ReferenceCheckAuthorization { get; set; }
        public bool DataPrivacyConsent { get; set; }
        public bool ElectronicSignatureConsent { get; set; }
        public bool EqualOpportunityAcknowledgment { get; set; }
        public bool ArbitrationAgreement { get; set; }
        public bool EmployeeHandbookAcknowledgment { get; set; }

        // Communication Preferences
        public string StatusUpdatesVia { get; set; } // Email, Text, Both
        public bool ReceiveJobAlerts { get; set; }
        public bool ShareProfileWithLocations { get; set; }
        public bool RetainForFutureOpenings { get; set; }
        public int RetentionPeriodDays { get; set; }

        // Electronic Signature
        public string Signature { get; set; }
        public DateTime? SignatureDate { get; set; }
        public string IpAddress { get; set; }
    }
}