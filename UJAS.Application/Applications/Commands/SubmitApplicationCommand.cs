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
    public class SubmitApplicationCommand : IRequest<ApiResponse<ApplicationDto>>
    {
        public int ApplicationId { get; set; }
    }

    public class SubmitApplicationCommandHandler : IRequestHandler<SubmitApplicationCommand, ApiResponse<ApplicationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailService _emailService;
        private readonly IDateTime _dateTime;

        public SubmitApplicationCommandHandler(
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

        public async Task<ApiResponse<ApplicationDto>> Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications
                .Include(a => a.Company)
                    .ThenInclude(c => c.Settings)
                .Include(a => a.ApplicantProfile)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.Id == request.ApplicationId && !a.IsDeleted, cancellationToken);

            if (application == null)
                throw new NotFoundException(nameof(Application), request.ApplicationId);

            if (application.Status != ApplicationStatus.Draft)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(application.Status),
                        ErrorMessage = "Application has already been submitted"
                    }
                });

            // Validate all required fields are answered
            var requiredFields = await _context.CompanyFields
                .Where(cf => cf.CompanyId == application.CompanyId &&
                           cf.IsEnabled && cf.IsRequired)
                .ToListAsync(cancellationToken);

            var answeredFieldIds = await _context.ApplicationAnswers
                .Where(aa => aa.ApplicationId == application.Id)
                .Select(aa => aa.CompanyFieldId)
                .ToListAsync(cancellationToken);

            var missingFields = requiredFields
                .Where(rf => !answeredFieldIds.Contains(rf.Id))
                .ToList();

            if (missingFields.Any())
            {
                var fieldNames = string.Join(", ", missingFields.Select(f => f.Label));
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = string.Empty,
                        ErrorMessage = $"Required fields missing: {fieldNames}"
                    }
                });
            }

            // Update application status
            application.Status = ApplicationStatus.Submitted;
            application.SubmissionDate = _dateTime.UtcNow;
            application.UpdatedBy = _currentUserService.UserId;
            application.UpdatedAt = _dateTime.UtcNow;

            // Calculate completion time
            if (application.CreatedAt.HasValue)
            {
                application.CompletionTimeMinutes = (int)(_dateTime.UtcNow - application.CreatedAt.Value).TotalMinutes;
            }

            // Create status history
            var statusHistory = new ApplicationStatusHistory
            {
                ApplicationId = application.Id,
                PreviousStatus = ApplicationStatus.Draft,
                NewStatus = ApplicationStatus.Submitted,
                ChangedBy = _currentUserService.UserId,
                Reason = "Application submitted by applicant"
            };

            _context.ApplicationStatusHistories.Add(statusHistory);

            // Send confirmation email
            if (application.Company.Settings.AutoReplyToApplicants)
            {
                await _emailService.SendApplicationConfirmationAsync(
                    application.ApplicantProfile.Email,
                    application.ApplicantProfile.FirstName,
                    application.ApplicationNumber,
                    application.Company.Name,
                    application.PositionAppliedFor);
            }

            // Notify company managers
            await NotifyManagersAsync(application, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ApplicationDto>(application);
            return ApiResponse<ApplicationDto>.SuccessResult(result, "Application submitted successfully");
        }

        private async Task NotifyManagersAsync(tApplication application, CancellationToken cancellationToken)
        {
            var managers = await _context.CompanyUsers
                .Include(cu => cu.User)
                .Where(cu => cu.CompanyId == application.CompanyId &&
                           (cu.LocationId == application.LocationId || cu.IsCompanyAdmin) &&
                           cu.IsActive)
                .Select(cu => cu.User.Email)
                .ToListAsync(cancellationToken);

            if (managers.Any())
            {
                await _emailService.SendNewApplicationNotificationAsync(
                    managers,
                    application.ApplicationNumber,
                    application.Company.Name,
                    application.PositionAppliedFor,
                    application.ApplicantProfile.FullName);
            }
        }
    }
}

