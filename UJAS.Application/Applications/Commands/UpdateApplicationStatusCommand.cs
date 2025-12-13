using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;
using UJAS.Application.Common.Interfaces;
using UJAS.Core.Entities.Application;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.Email;

namespace UJAS.Application.Applications.Commands
{
    public class UpdateApplicationStatusCommand : IRequest<ApiResponse<ApplicationDto>>
    {
        public int ApplicationId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, ApiResponse<ApplicationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailService _emailService;
        private readonly IDateTime _dateTime;

        public UpdateApplicationStatusCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IEmailService emailService,
            IDateTime dateTime)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _emailService = emailService;
            _dateTime = dateTime;
        }

        public async Task<ApiResponse<ApplicationDto>> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications
                .Include(a => a.ApplicantProfile)
                .Include(a => a.Company)
                .Include(a => a.StatusHistory)
                .FirstOrDefaultAsync(a => a.Id == request.ApplicationId && !a.IsDeleted, cancellationToken);

            if (application == null)
                throw new NotFoundException(nameof(Application), request.ApplicationId);

            var previousStatus = application.Status;

            // Update application
            application.Status = request.Status;
            application.UpdatedBy = _currentUserService.UserId;
            application.UpdatedAt = _dateTime.UtcNow;

            // Set specific dates based on status
            switch (request.Status)
            {
                case ApplicationStatus.UnderReview:
                    application.ReviewDate = _dateTime.UtcNow;
                    break;
                case ApplicationStatus.Accepted:
                case ApplicationStatus.Rejected:
                    application.DecisionDate = _dateTime.UtcNow;
                    if (request.Status == ApplicationStatus.Rejected)
                        application.RejectionReason = request.Reason;
                    break;
            }

            // Create status history
            var statusHistory = new ApplicationStatusHistory
            {
                ApplicationId = application.Id,
                PreviousStatus = previousStatus,
                NewStatus = request.Status,
                ChangedBy = _currentUserService.UserId,
                Reason = request.Reason,
                Notes = request.Notes
            };

            _context.ApplicationStatusHistories.Add(statusHistory);

            // Send notification to applicant
            await SendStatusNotificationAsync(application, previousStatus, request.Status, request.Reason, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ApplicationDto>(application);
            return ApiResponse<ApplicationDto>.SuccessResult(result, $"Application status updated to {request.Status}");
        }

        private async Task SendStatusNotificationAsync(
            tApplication application,
            ApplicationStatus previousStatus,
            ApplicationStatus newStatus,
            string reason,
            CancellationToken cancellationToken)
        {
            // Don't send notifications for internal status changes
            if (newStatus == ApplicationStatus.UnderReview ||
                newStatus == ApplicationStatus.BackgroundCheck)
                return;

            var emailData = new
            {
                ApplicantName = application.ApplicantProfile.FullName,
                ApplicationNumber = application.ApplicationNumber,
                CompanyName = application.Company.Name,
                Position = application.PositionAppliedFor,
                PreviousStatus = previousStatus.ToString(),
                NewStatus = newStatus.ToString(),
                Reason = reason,
                Date = _dateTime.UtcNow.ToString("MM/dd/yyyy")
            };

            await _emailService.SendStatusUpdateNotificationAsync(
                application.ApplicantProfile.Email,
                application.ApplicantProfile.FirstName,
                emailData);
        }
    }
}
