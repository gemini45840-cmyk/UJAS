using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Common.Models;
using UJAS.Application.Companies.Commands;
using UJAS.Application.Companies.Dtos;
using UJAS.Application.Companies.Queries;

namespace UJAS.API.Controllers.Companies
{
    [ApiVersion("1.0")]
    public class CompaniesController : BaseApiController
    {
        /// <summary>
        /// Get all companies (System Admin only)
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get all companies", Description = "Returns a paginated list of all companies. System administrators only.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<PaginatedResponse<CompanyDto>>))]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(403, "Forbidden")]
        public async Task<IActionResult> GetCompanies([FromQuery] GetCompaniesQuery query)
        {
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get company by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Get company by ID", Description = "Returns company details by ID. Accessible by system admin or company admin of the company.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<CompanyDto>))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> GetCompany(int id, [FromQuery] bool includeLocations = false, [FromQuery] bool includeStatistics = false)
        {
            var query = new GetCompanyByIdQuery
            {
                CompanyId = id,
                IncludeLocations = includeLocations,
                IncludeStatistics = includeStatistics
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Create a new company (System Admin only)
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Create a new company", Description = "Creates a new company. System administrators only.")]
        [SwaggerResponse(201, "Company created successfully", typeof(ApiResponse<CompanyDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto companyDto)
        {
            var command = new CreateCompanyCommand { Company = companyDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetCompany), new { id = result.Data.Id }, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update company
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Update company", Description = "Updates company details. Accessible by system admin or company admin of the company.")]
        [SwaggerResponse(200, "Company updated successfully", typeof(ApiResponse<CompanyDto>))]
        [SwaggerResponse(400, "Validation error")]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto companyDto)
        {
            var command = new UpdateCompanyCommand { CompanyId = id, Company = companyDto };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Delete company (System Admin only)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Delete company", Description = "Soft deletes a company. System administrators only.")]
        [SwaggerResponse(204, "Company deleted successfully")]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var command = new DeleteCompanyCommand { CompanyId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
                return NoContent();

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get company widget settings
        /// </summary>
        [HttpGet("{id}/widget-settings")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get company widget settings", Description = "Returns widget configuration for embedding job application forms.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<CompanyWidgetSettingsDto>))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> GetWidgetSettings(int id)
        {
            var query = new GetCompanyWidgetSettingsQuery { CompanyId = id };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Regenerate widget embed code
        /// </summary>
        [HttpPost("{id}/regenerate-widget-code")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Regenerate widget embed code", Description = "Regenerates the widget embed code for a company.")]
        [SwaggerResponse(200, "Widget code regenerated", typeof(ApiResponse<string>))]
        public async Task<IActionResult> RegenerateWidgetCode(int id)
        {
            var command = new RegenerateWidgetCodeCommand { CompanyId = id };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get company analytics
        /// </summary>
        [HttpGet("{id}/analytics")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Get company analytics", Description = "Returns analytics and metrics for a company.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<CompanyAnalyticsDto>))]
        public async Task<IActionResult> GetAnalytics(int id, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetCompanyAnalyticsQuery
            {
                CompanyId = id,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update company settings
        /// </summary>
        [HttpPut("{id}/settings")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Update company settings", Description = "Updates company-specific settings.")]
        [SwaggerResponse(200, "Settings updated", typeof(ApiResponse<CompanySettingsDto>))]
        public async Task<IActionResult> UpdateSettings(int id, [FromBody] CompanySettingsDto settings)
        {
            var command = new UpdateCompanySettingsCommand
            {
                CompanyId = id,
                Settings = settings
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }
    }
}