using Hangfire;
using Hangfire.SqlServer;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Services;
using UJAS.Infrastructure.Services.Email;

namespace UJAS.API.Services
{
    public interface IBackgroundJobService
    {
        string EnqueueApplicationStatusUpdate(int applicationId, string newStatus, string changedBy);
        string ScheduleApplicationReminder(int applicationId, DateTime remindAt);
        string EnqueueEmailNotification(string toEmail, string subject, string body);
        string EnqueueDataRetentionCleanup();
    }

    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ILogger<BackgroundJobService> _logger;

        public BackgroundJobService(
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            ILogger<BackgroundJobService> logger)
        {
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _logger = logger;
        }

        public string EnqueueApplicationStatusUpdate(int applicationId, string newStatus, string changedBy)
        {
            try
            {
                return _backgroundJobClient.Enqueue<IApplicationService>(
                    service => service.UpdateApplicationStatusAsync(applicationId, newStatus, changedBy));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enqueueing application status update");
                return null;
            }
        }

        public string ScheduleApplicationReminder(int applicationId, DateTime remindAt)
        {
            try
            {
                return _backgroundJobClient.Schedule<IApplicationService>(
                    service => service.SendApplicationReminderAsync(applicationId),
                    remindAt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error scheduling application reminder");
                return null;
            }
        }

        public string EnqueueEmailNotification(string toEmail, string subject, string body)
        {
            try
            {
                return _backgroundJobClient.Enqueue<IEmailService>(
                    service => service.SendEmailAsync(toEmail, subject, body));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enqueueing email notification");
                return null;
            }
        }

        public string EnqueueDataRetentionCleanup()
        {
            try
            {
                return _backgroundJobClient.Enqueue<IDataRetentionService>(
                    service => service.CleanupOldDataAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enqueueing data retention cleanup");
                return null;
            }
        }

        public void ScheduleRecurringJobs()
        {
            // Schedule daily data retention cleanup at 2 AM
            _recurringJobManager.AddOrUpdate<IDataRetentionService>(
                "data-retention-cleanup",
                service => service.CleanupOldDataAsync(),
                Cron.Daily(2));

            // Schedule application status check every hour
            _recurringJobManager.AddOrUpdate<IApplicationService>(
                "application-status-check",
                service => service.CheckStaleApplicationsAsync(),
                Cron.Hourly);

            // Schedule weekly analytics generation on Sunday at 3 AM
            _recurringJobManager.AddOrUpdate<IAnalyticsService>(
                "weekly-analytics",
                service => service.GenerateWeeklyAnalyticsAsync(),
                Cron.Weekly(DayOfWeek.Sunday, 3));

            _logger.LogInformation("Recurring jobs scheduled successfully");
        }
    }
}