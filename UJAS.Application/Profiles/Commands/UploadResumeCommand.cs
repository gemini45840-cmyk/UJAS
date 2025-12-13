using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Profiles.Dtos;
using UJAS.Core.Entities.Profile;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.FileStorage;

namespace UJAS.Application.Profiles.Commands
{
    public class UploadResumeCommand : IRequest<ApiResponse<DocumentDto>>
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string FileData { get; set; } // Base64 encoded
        public bool SetAsPrimary { get; set; } = true;
        public VisibilitySetting Visibility { get; set; } = VisibilitySetting.VisibleToAll;
    }

    public class UploadResumeCommandHandler : IRequestHandler<UploadResumeCommand, ApiResponse<DocumentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IDateTime _dateTime;
        private readonly IResumeParserService _resumeParserService;

        public UploadResumeCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IFileStorageService fileStorageService,
            IDateTime dateTime,
            IResumeParserService resumeParserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileStorageService = fileStorageService;
            _dateTime = dateTime;
            _resumeParserService = resumeParserService;
        }

        public async Task<ApiResponse<DocumentDto>> Handle(UploadResumeCommand request, CancellationToken cancellationToken)
        {
            var profile = await _context.ApplicantProfiles
                .Include(p => p.Documents)
                .FirstOrDefaultAsync(p => p.UserId == _currentUserService.UserId, cancellationToken);

            if (profile == null)
                throw new NotFoundException(nameof(ApplicantProfile), _currentUserService.UserId);

            // Validate file size (max 10MB)
            if (request.FileSize > 10 * 1024 * 1024)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.FileData),
                        ErrorMessage = "File size must be less than 10MB"
                    }
                });

            // Validate file type
            var allowedTypes = new[] { "application/pdf", "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "text/plain" };

            if (!allowedTypes.Contains(request.ContentType.ToLower()))
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.ContentType),
                        ErrorMessage = "Only PDF, DOC, DOCX, and TXT files are allowed"
                    }
                });

            // Save file
            var fileBytes = Convert.FromBase64String(request.FileData);
            var filePath = await _fileStorageService.SaveFileAsync(request.FileName, fileBytes, request.ContentType);

            // Parse resume if supported
            ResumeParsedData parsedData = null;
            if (request.ContentType == "application/pdf" ||
                request.ContentType == "application/msword" ||
                request.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                parsedData = await _resumeParserService.ParseResumeAsync(fileBytes, request.ContentType);
            }

            // Update existing primary resume to non-primary
            if (request.SetAsPrimary)
            {
                var existingPrimary = profile.Documents.FirstOrDefault(d =>
                    d.DocumentType == DocumentType.Resume && d.IsPrimary);

                if (existingPrimary != null)
                {
                    existingPrimary.IsPrimary = false;
                }
            }

            // Create document record
            var document = new Document
            {
                ApplicantProfileId = profile.Id,
                DocumentType = DocumentType.Resume,
                FileName = request.FileName,
                FilePath = filePath,
                ContentType = request.ContentType,
                FileSize = request.FileSize,
                Description = "Resume/CV",
                IsPrimary = request.SetAsPrimary,
                UploadDate = _dateTime.UtcNow
            };

            _context.Documents.Add(document);

            // Update profile resume info
            profile.ResumeFilePath = filePath;
            profile.ResumeLastUpdated = _dateTime.UtcNow;
            profile.ResumeVisibility = request.Visibility;

            // Update profile with parsed data if available
            if (parsedData != null)
            {
                UpdateProfileWithParsedData(profile, parsedData);
            }

            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<DocumentDto>(document);
            return ApiResponse<DocumentDto>.SuccessResult(result, "Resume uploaded successfully");
        }

        private void UpdateProfileWithParsedData(ApplicantProfile profile, ResumeParsedData parsedData)
        {
            // Update basic info if not already set
            if (string.IsNullOrEmpty(profile.FirstName) && !string.IsNullOrEmpty(parsedData.FirstName))
                profile.FirstName = parsedData.FirstName;

            if (string.IsNullOrEmpty(profile.LastName) && !string.IsNullOrEmpty(parsedData.LastName))
                profile.LastName = parsedData.LastName;

            if (string.IsNullOrEmpty(profile.Email) && !string.IsNullOrEmpty(parsedData.Email))
                profile.Email = parsedData.Email;

            if (string.IsNullOrEmpty(profile.MobilePhone) && !string.IsNullOrEmpty(parsedData.Phone))
                profile.MobilePhone = parsedData.Phone;

            // Add parsed skills
            foreach (var skill in parsedData.Skills)
            {
                if (!profile.Skills.Any(s => s.Name == skill))
                {
                    profile.Skills.Add(new Skill
                    {
                        Name = skill,
                        SkillType = SkillType.Technical,
                        DisplayOrder = profile.Skills.Count + 1
                    });
                }
            }

            // Add parsed work experience
            foreach (var exp in parsedData.WorkExperience)
            {
                profile.WorkExperiences.Add(new WorkExperience
                {
                    EmployerName = exp.Company,
                    JobTitle = exp.Position,
                    StartDate = exp.StartDate,
                    EndDate = exp.EndDate,
                    IsCurrentEmployer = exp.IsCurrent,
                    JobResponsibilities = exp.Description,
                    DisplayOrder = profile.WorkExperiences.Count + 1
                });
            }

            // Add parsed education
            foreach (var edu in parsedData.Education)
            {
                profile.EducationHistories.Add(new EducationHistory
                {
                    InstitutionName = edu.Institution,
                    DegreeCertificate = edu.Degree,
                    FieldOfStudy = edu.Field,
                    GraduationDate = edu.GraduationDate,
                    DisplayOrder = profile.EducationHistories.Count + 1
                });
            }
        }
    }
}
