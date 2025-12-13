using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UJAS.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email, string name, string confirmationToken);
        Task SendPasswordResetEmailAsync(string email, string resetToken);
        Task SendApplicationConfirmationAsync(string email, string name, string applicationNumber, string companyName, string position);
        Task SendNewApplicationNotificationAsync(List<string> managerEmails, string applicationNumber, string companyName, string position, string applicantName);
        Task SendStatusUpdateNotificationAsync(string email, string name, object emailData);
        Task SendAssessmentInvitationAsync(string email, string name, string assessmentName, string companyName, string assessmentUrl);
    }
}