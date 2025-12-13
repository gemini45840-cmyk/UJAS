using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Applications.Dtos;
using UJAS.Application.Common.DTOs;
using UJAS.Application.Common.Interfaces;
using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Field;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.FileStorage;

namespace UJAS.Application.Applications.Commands
{
    public class CreateApplicationCommand : IRequest<ApiResponse<ApplicationDto>>
    {
        public int CompanyId { get; set; }
        public int LocationId { get; set; }
        public string PositionAppliedFor { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new();
        public List<FileAttachmentDto> Documents { get; set; } = new();
        public bool UseProfileData { get; set; } = true;
        public bool AcceptTerms { get; set; }
        public string ReferralSource { get; set; }
        public string CampaignTrackingCode { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }

    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, ApiResponse<ApplicationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileStorageService _fileStorageService;

        public CreateApplicationCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IFileStorageService fileStorageService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileStorageService = fileStorageService;
        }

        public async Task<ApiResponse<ApplicationDto>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            // Validate company and location
            var company = await _context.Companies
                .Include(c => c.Locations)
                .Include(c => c.Settings)
                .FirstOrDefaultAsync(c => c.Id == request.CompanyId && c.IsActive, cancellationToken);

            if (company == null)
                throw new NotFoundException(nameof(tCompany), request.CompanyId);

            var location = company.Locations.FirstOrDefault(l => l.Id == request.LocationId && l.IsActive);
            if (location == null)
                throw new NotFoundException(nameof(Location), request.LocationId);

            // Get applicant profile
            var applicantProfile = await _context.ApplicantProfiles
                .FirstOrDefaultAsync(p => p.UserId == _currentUserService.UserId, cancellationToken);

            if (applicantProfile == null)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = string.Empty,
                        ErrorMessage = "Applicant profile not found"
                    }
                });

            // Validate terms acceptance
            if (!request.AcceptTerms)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.AcceptTerms),
                        ErrorMessage = "You must accept the terms and conditions"
                    }
                });

            // Get next application number
            var sequenceNumber = await GetNextSequenceNumber(company.Id);
            var applicationNumber = ApplicationNumberGenerator.Generate(company.Id, sequenceNumber);

            // Create application
            var application = new tApplication
            {
                ApplicationNumber = applicationNumber,
                ApplicantProfileId = applicantProfile.Id,
                CompanyId = company.Id,
                LocationId = location.Id,
                PositionAppliedFor = request.PositionAppliedFor,
                Status = ApplicationStatus.Draft,
                ReferralSource = request.ReferralSource,
                CampaignTrackingCode = request.CampaignTrackingCode,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent,
                CreatedBy = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow,
                RetentionEndDate = DateTime.UtcNow.AddDays(company.Settings.ApplicationRetentionDays)
            };

            // Get required fields for this company/location
            var requiredFields = await GetRequiredFields(company.Id, location.Id);

            // Validate and create answers
            var answers = new List<ApplicationAnswer>();
            foreach (var field in requiredFields)
            {
                if (request.Answers.TryGetValue(field.Id, out var answer))
                {
                    // Validate answer
                    ValidateAnswer(field, answer);

                    answers.Add(new ApplicationAnswer
                    {
                        Application = application,
                        CompanyFieldId = field.Id,
                        AnswerText = answer,
                        AnswerDate = DateTime.UtcNow
                    });
                }
                else if (field.IsRequired)
                {
                    throw new ValidationException(new List<ValidationError>
                    {
                        new ValidationError
                        {
                            PropertyName = field.Label,
                            ErrorMessage = $"{field.Label} is required"
                        }
                    });
                }
            }

            // Handle documents
            var documents = new List<ApplicationDocument>();
            foreach (var docDto in request.Documents)
            {
                var filePath = await _fileStorageService.SaveFileAsync(
                    docDto.FileName,
                    Convert.FromBase64String(docDto.FileData),
                    docDto.ContentType);

                documents.Add(new ApplicationDocument
                {
                    Application = application,
                    DocumentType = DocumentType.Other,
                    FileName = docDto.FileName,
                    FilePath = filePath,
                    ContentType = docDto.ContentType,
                    FileSize = docDto.FileSize,
                    UploadDate = DateTime.UtcNow
                });
            }

            // Save everything
            _context.Applications.Add(application);
            _context.ApplicationAnswers.AddRange(answers);
            _context.ApplicationDocuments.AddRange(documents);

            await _context.SaveChangesAsync(cancellationToken);

            // Create initial status history
            var statusHistory = new ApplicationStatusHistory
            {
                ApplicationId = application.Id,
                PreviousStatus = ApplicationStatus.Draft,
                NewStatus = ApplicationStatus.Draft,
                ChangedBy = _currentUserService.UserId,
                Reason = "Application created"
            };

            _context.ApplicationStatusHistories.Add(statusHistory);
            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ApplicationDto>(application);
            return ApiResponse<ApplicationDto>.SuccessResult(result, "Application created successfully");
        }

        private async Task<int> GetNextSequenceNumber(int companyId)
        {
            var lastApplication = await _context.Applications
                .Where(a => a.CompanyId == companyId)
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync();

            return lastApplication != null ? lastApplication.Id + 1 : 1;
        }

        private async Task<List<CompanyField>> GetRequiredFields(int companyId, int locationId)
        {
            var query = _context.CompanyFields
                .Include(cf => cf.LocationFields)
                .Where(cf => cf.CompanyId == companyId && cf.IsEnabled);

            if (!await query.AnyAsync(cf => cf.ApplyToAllLocations))
            {
                query = query.Where(cf => cf.LocationFields.Any(lf => lf.LocationId == locationId));
            }

            return await query.ToListAsync();
        }

        private void ValidateAnswer(CompanyField field, string answer)
        {
            // Implement field-specific validation
            switch (field.FieldType)
            {
                case FieldType.Email:
                    if (!ValidationHelper.IsValidEmail(answer))
                        throw new ValidationException(new List<ValidationError>
                        {
                            new ValidationError
                            {
                                PropertyName = field.Label,
                                ErrorMessage = "Invalid email format"
                            }
                        });
                    break;
                case FieldType.Phone:
                    if (!ValidationHelper.IsValidPhoneNumber(answer))
                        throw new ValidationException(new List<ValidationError>
                        {
                            new ValidationError
                            {
                                PropertyName = field.Label,
                                ErrorMessage = "Invalid phone number format"
                            }
                        });
                    break;
                case FieldType.Url:
                    if (!ValidationHelper.IsValidUrl(answer))
                        throw new ValidationException(new List<ValidationError>
                        {
                            new ValidationError
                            {
                                PropertyName = field.Label,
                                ErrorMessage = "Invalid URL format"
                            }
                        });
                    break;
                case FieldType.Number:
                    if (!decimal.TryParse(answer, out _))
                        throw new ValidationException(new List<ValidationError>
                        {
                            new ValidationError
                            {
                                PropertyName = field.Label,
                                ErrorMessage = "Must be a valid number"
                            }
                        });
                    break;
                    // Add more validation as needed
            }
        }
    }
}
