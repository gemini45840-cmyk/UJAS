using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using UJAS.Core.Entities.System;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Infrastructure.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, bool isHtml = true);
        Task SendTemplateEmailAsync(string toEmail, string templateName, object templateData);
        Task SendApplicationConfirmationAsync(int applicationId);
        Task SendStatusUpdateAsync(int applicationId, string newStatus, string comments = null);
        Task SendAssessmentInvitationAsync(int applicationAssessmentId);
    }

    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendGridEmailService> _logger;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly ApplicationDbContext _context;

        public SendGridEmailService(
            IConfiguration configuration,
            ILogger<SendGridEmailService> logger,
            IRepository<EmailTemplate> emailTemplateRepository,
            ApplicationDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _emailTemplateRepository = emailTemplateRepository;
            _context = context;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message, bool isHtml = true)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var fromEmail = _configuration["SendGrid:FromEmail"];
            var fromName = _configuration["SendGrid:FromName"];

            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogWarning("SendGrid API key is not configured");
                return;
            }

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject,
                isHtml ? null : message,
                isHtml ? message : null);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Body.ReadAsStringAsync();
                _logger.LogError("Failed to send email: {StatusCode} - {Body}",
                    response.StatusCode, body);
            }
        }

        public async Task SendTemplateEmailAsync(string toEmail, string templateName, object templateData)
        {
            var template = await _emailTemplateRepository.GetSingleAsync(
                et => et.TemplateName == templateName && et.IsActive);

            if (template == null)
            {
                _logger.LogWarning("Email template not found: {TemplateName}", templateName);
                return;
            }

            var message = ReplaceTemplateVariables(template.Body, templateData);
            var subject = ReplaceTemplateVariables(template.Subject, templateData);

            await SendEmailAsync(toEmail, subject, message, template.IsHtml);
        }

        public async Task SendApplicationConfirmationAsync(int applicationId)
        {
            var application = await _context.Applications
                .Include(a => a.ApplicantProfile)
                .Include(a => a.Company)
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
            {
                _logger.LogError("Application not found: {ApplicationId}", applicationId);
                return;
            }

            var templateData = new
            {
                ApplicantName = $"{application.ApplicantProfile.FirstName} {application.ApplicantProfile.LastName}",
                CompanyName = application.Company.Name,
                Position = application.PositionAppliedFor,
                ApplicationNumber = application.ApplicationNumber,
                SubmissionDate = application.SubmissionDate?.ToString("MMMM dd, yyyy"),
                CompanyContactEmail = application.Company.Email,
                CompanyPhone = application.Company.Phone
            };

            await SendTemplateEmailAsync(
                application.ApplicantProfile.Email,
                "ApplicationConfirmation",
                templateData);
        }

        public async Task SendStatusUpdateAsync(int applicationId, string newStatus, string comments = null)
        {
            var application = await _context.Applications
                .Include(a => a.ApplicantProfile)
                .Include(a => a.Company)
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
            {
                _logger.LogError("Application not found: {ApplicationId}", applicationId);
                return;
            }

            var templateData = new
            {
                ApplicantName = $"{application.ApplicantProfile.FirstName} {application.ApplicantProfile.LastName}",
                CompanyName = application.Company.Name,
                Position = application.PositionAppliedFor,
                ApplicationNumber = application.ApplicationNumber,
                NewStatus = newStatus,
                Comments = comments ?? string.Empty,
                UpdateDate = DateTime.UtcNow.ToString("MMMM dd, yyyy")
            };

            await SendTemplateEmailAsync(
                application.ApplicantProfile.Email,
                "ApplicationStatusUpdate",
                templateData);
        }

        public async Task SendAssessmentInvitationAsync(int applicationAssessmentId)
        {
            var assessment = await _context.ApplicationAssessments
                .Include(aa => aa.Application)
                .ThenInclude(a => a.ApplicantProfile)
                .Include(aa => aa.Assessment)
                .FirstOrDefaultAsync(aa => aa.Id == applicationAssessmentId);

            if (assessment == null)
            {
                _logger.LogError("Application assessment not found: {AssessmentId}", applicationAssessmentId);
                return;
            }

            var templateData = new
            {
                ApplicantName = $"{assessment.Application.ApplicantProfile.FirstName} {assessment.Application.ApplicantProfile.LastName}",
                AssessmentName = assessment.Assessment.Name,
                AssessmentType = assessment.Assessment.AssessmentType.ToString(),
                TimeLimit = assessment.Assessment.TimeLimitMinutes,
                Instructions = assessment.Assessment.Instructions,
                AssessmentUrl = $"{_configuration["AppSettings:BaseUrl"]}/assessments/{assessment.Id}",
                DueDate = assessment.ExpiresAt?.ToString("MMMM dd, yyyy")
            };

            await SendTemplateEmailAsync(
                assessment.Application.ApplicantProfile.Email,
                "AssessmentInvitation",
                templateData);
        }

        private string ReplaceTemplateVariables(string template, object data)
        {
            if (string.IsNullOrEmpty(template) || data == null)
                return template;

            var properties = data.GetType().GetProperties();
            foreach (var property in properties)
            {
                var placeholder = $"{{{{{property.Name}}}}}";
                var value = property.GetValue(data)?.ToString() ?? string.Empty;
                template = template.Replace(placeholder, value);
            }

            return template;
        }
    }
}