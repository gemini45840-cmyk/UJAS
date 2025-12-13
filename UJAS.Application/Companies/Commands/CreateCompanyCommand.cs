using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Base;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Application.Companies.Dtos;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Companies.Commands
{
    public class CreateCompanyCommand : BaseCommand<CompanyDto>
    {
        public CreateCompanyDto Company { get; set; }
    }

    public class CreateCompanyCommandHandler : BaseHandler, IRequestHandler<CreateCompanyCommand, ApiResponse<CompanyDto>>
    {
        public CreateCompanyCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            IDateTimeService dateTime,
            ILogger<CreateCompanyCommandHandler> logger)
            : base(unitOfWork, mapper, currentUser, dateTime, logger)
        {
        }

        public async Task<ApiResponse<CompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Only system administrators can create companies
                if (!_currentUser.IsSystemAdmin)
                    return await HandleUnauthorizedAsync<CompanyDto>("create company");

                // Check if company with same name already exists
                var existingCompany = await _unitOfWork.Repository<Core.Entities.Company.Company>()
                    .GetSingleAsync(c => c.Name == request.Company.Name && !c.IsDeleted);

                if (existingCompany != null)
                    return ApiResponse<CompanyDto>.FailureResponse(
                        "A company with this name already exists");

                // Map and create company
                var company = _mapper.Map<Core.Entities.Company.Company>(request.Company);
                company.CreatedAt = _dateTime.UtcNow;
                company.CreatedBy = _currentUser.Email ?? "system";
                company.IsActive = true;

                // Create settings
                if (request.Company.Settings != null)
                {
                    company.Settings = _mapper.Map<Core.Entities.Company.CompanySettings>(request.Company.Settings);
                }

                await _unitOfWork.Repository<Core.Entities.Company.Company>().AddAsync(company);
                await _unitOfWork.SaveChangesAsync();

                // Generate widget embed code
                company.WidgetEmbedCode = GenerateWidgetEmbedCode(company);
                await _unitOfWork.SaveChangesAsync();

                var companyDto = _mapper.Map<CompanyDto>(company);
                return ApiResponse<CompanyDto>.SuccessResponse(companyDto, "Company created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                return ApiResponse<CompanyDto>.FailureResponse("Error creating company");
            }
        }

        private string GenerateWidgetEmbedCode(Core.Entities.Company.Company company)
        {
            var widgetCode = $@"
<!-- UJAS Application Widget -->
<div id='ujas-widget-{company.Id}'></div>
<script>
    (function() {{
        var widgetConfig = {{
            companyId: {company.Id},
            primaryColor: '{company.PrimaryColor}',
            secondaryColor: '{company.SecondaryColor}',
            logoUrl: '{company.LogoUrl ?? ""}'
        }};
        
        var script = document.createElement('script');
        script.src = 'https://widget.ujas.com/widget.js?' + new Date().getTime();
        script.async = true;
        script.onload = function() {{
            window.initUJASWidget('ujas-widget-{company.Id}', widgetConfig);
        }};
        document.head.appendChild(script);
    }})();
</script>";

            return widgetCode;
        }
    }
}