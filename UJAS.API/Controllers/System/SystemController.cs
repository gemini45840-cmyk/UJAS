using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.System.Commands;
using UJAS.Application.System.Dtos;
using UJAS.Application.System.Queries;

namespace UJAS.API.Controllers.System
{
    [ApiVersion("1.0")]
    public class SystemController : BaseApiController
    {
        /// <summary>
        /// Get system health
        /// </summary>
        [HttpGet("health")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get system health", Description = "Returns system health status.")]
        [SwaggerResponse(200, "System healthy", typeof(ApiResponse<SystemHealthDto>))]
        public async Task<IActionResult> GetHealth()
        {
            var query = new GetSystemHealthQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get system statistics
        /// </summary>
        [HttpGet("statistics")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get system statistics", Description = "Returns system-wide statistics.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<SystemStatisticsDto>))]
        public async Task<IActionResult> GetStatistics([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetSystemStatisticsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get audit logs
        /// </summary>
        [HttpGet("audit-logs")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get audit logs", Description = "Returns system audit logs.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<PaginatedResponse<AuditLogDto>>))]
        public async Task<IActionResult> GetAuditLogs([FromQuery] GetAuditLogsQuery query)
        {
            var result = await Mediator.Send(query);
            return PaginatedResult(result.Data);
        }

        /// <summary>
        /// Get system settings
        /// </summary>
        [HttpGet("settings")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get system settings", Description = "Returns all system settings.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<SystemSettingDto>>))]
        public async Task<IActionResult> GetSettings()
        {
            var query = new GetSystemSettingsQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update system setting
        /// </summary>
        [HttpPut("settings/{key}")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Update system setting", Description = "Updates a system setting.")]
        [SwaggerResponse(200, "Setting updated", typeof(ApiResponse<SystemSettingDto>))]
        public async Task<IActionResult> UpdateSetting(string key, [FromBody] UpdateSystemSettingDto settingDto)
        {
            var command = new UpdateSystemSettingCommand
            {
                SettingKey = key,
                SettingValue = settingDto.Value
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get email templates
        /// </summary>
        [HttpGet("email-templates")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get email templates", Description = "Returns all email templates.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<EmailTemplateDto>>))]
        public async Task<IActionResult> GetEmailTemplates()
        {
            var query = new GetEmailTemplatesQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update email template
        /// </summary>
        [HttpPut("email-templates/{id}")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Update email template", Description = "Updates an email template.")]
        [SwaggerResponse(200, "Template updated", typeof(ApiResponse<EmailTemplateDto>))]
        public async Task<IActionResult> UpdateEmailTemplate(int id, [FromBody] UpdateEmailTemplateDto templateDto)
        {
            var command = new UpdateEmailTemplateCommand
            {
                TemplateId = id,
                Subject = templateDto.Subject,
                Body = templateDto.Body,
                IsHtml = templateDto.IsHtml
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Send test email
        /// </summary>
        [HttpPost("email-templates/{id}/test")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Send test email", Description = "Sends a test email using the template.")]
        [SwaggerResponse(200, "Test email sent", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> SendTestEmail(int id, [FromBody] SendTestEmailDto testDto)
        {
            var command = new SendTestEmailCommand
            {
                TemplateId = id,
                ToEmail = testDto.ToEmail,
                TestData = testDto.TestData
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Run data retention cleanup
        /// </summary>
        [HttpPost("data-retention/cleanup")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Run data retention", Description = "Runs data retention cleanup manually.")]
        [SwaggerResponse(200, "Cleanup completed", typeof(ApiResponse<DataRetentionResultDto>))]
        public async Task<IActionResult> RunDataRetention()
        {
            var command = new RunDataRetentionCommand();
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Generate API key
        /// </summary>
        [HttpPost("api-keys")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Generate API key", Description = "Generates a new API key for external integrations.")]
        [SwaggerResponse(201, "API key generated", typeof(ApiResponse<ApiKeyDto>))]
        public async Task<IActionResult> GenerateApiKey([FromBody] GenerateApiKeyDto apiKeyDto)
        {
            var command = new GenerateApiKeyCommand
            {
                Name = apiKeyDto.Name,
                CompanyId = apiKeyDto.CompanyId,
                ExpiresAt = apiKeyDto.ExpiresAt
            };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return Created("", result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get API keys
        /// </summary>
        [HttpGet("api-keys")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get API keys", Description = "Returns all API keys.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<ApiKeyDto>>))]
        public async Task<IActionResult> GetApiKeys()
        {
            var query = new GetApiKeysQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }
    }

    public class UpdateSystemSettingDto
    {
        public string Value { get; set; }
    }

    public class UpdateEmailTemplateDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }

    public class SendTestEmailDto
    {
        public string ToEmail { get; set; }
        public Dictionary<string, object> TestData { get; set; } = new();
    }

    public class GenerateApiKeyDto
    {
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}