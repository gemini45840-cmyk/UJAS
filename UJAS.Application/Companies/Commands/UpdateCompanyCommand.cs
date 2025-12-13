using System.Text.RegularExpressions;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Companies.Dtos;
using UJAS.Infrastructure.Data;

namespace UJAS.Application.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest<ApiResponse<CompanyDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TimeZone { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, ApiResponse<CompanyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCompanyCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<CompanyDto>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .Include(c => c.Settings)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (company == null)
                throw new NotFoundException(nameof(tCompany), request.Id);

            // Update properties
            company.Name = request.Name ?? company.Name;
            company.LegalName = request.LegalName ?? company.LegalName;
            company.Website = request.Website ?? company.Website;
            company.Industry = request.Industry ?? company.Industry;
            company.Description = request.Description ?? company.Description;
            company.LogoUrl = request.LogoUrl ?? company.LogoUrl;
            company.PrimaryColor = request.PrimaryColor ?? company.PrimaryColor;
            company.SecondaryColor = request.SecondaryColor ?? company.SecondaryColor;
            company.TimeZone = request.TimeZone ?? company.TimeZone;
            company.IsActive = request.IsActive ?? company.IsActive;
            company.UpdatedBy = _currentUserService.UserId;
            company.UpdatedAt = DateTime.UtcNow;

            // Update widget embed code if colors changed
            if (!string.IsNullOrEmpty(request.PrimaryColor) || !string.IsNullOrEmpty(request.SecondaryColor))
            {
                company.WidgetEmbedCode = UpdateWidgetColors(company.WidgetEmbedCode,
                    request.PrimaryColor, request.SecondaryColor);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ApiResponse<CompanyDto>.SuccessResult(
                _mapper.Map<CompanyDto>(company),
                "Company updated successfully");
        }

        private string UpdateWidgetColors(string embedCode, string primaryColor, string secondaryColor)
        {
            // Simple regex replacement for widget colors
            if (!string.IsNullOrEmpty(primaryColor))
                embedCode = Regex.Replace(embedCode, @"data-primary-color='[^']*'",
                    $"data-primary-color='{primaryColor}'");

            if (!string.IsNullOrEmpty(secondaryColor))
                embedCode = Regex.Replace(embedCode, @"data-secondary-color='[^']*'",
                    $"data-secondary-color='{secondaryColor}'");

            return embedCode;
        }
    }
}