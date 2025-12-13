using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Common.Models;
using UJAS.Application.Widget.Commands;
using UJAS.Application.Widget.Dtos;
using UJAS.Application.Widget.Queries;

namespace UJAS.API.Controllers.Widget
{
    [ApiVersion("1.0")]
    [EnableCors("WidgetPolicy")]
    [ApiExplorerSettings(GroupName = "Widget")]
    public class WidgetController : PublicApiController
    {
        /// <summary>
        /// Get widget configuration
        /// </summary>
        [HttpGet("config/{companyId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get widget config", Description = "Returns widget configuration for embedding.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<WidgetConfigDto>))]
        [SwaggerResponse(404, "Company not found")]
        public async Task<IActionResult> GetWidgetConfig(int companyId, [FromQuery] string? locationCode = null)
        {
            var query = new GetWidgetConfigQuery
            {
                CompanyId = companyId,
                LocationCode = locationCode
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get available positions for company/location
        /// </summary>
        [HttpGet("positions/{companyId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get available positions", Description = "Returns available positions for a company/location.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<string>>))]
        public async Task<IActionResult> GetPositions(int companyId, [FromQuery] int? locationId = null)
        {
            var query = new GetAvailablePositionsQuery
            {
                CompanyId = companyId,
                LocationId = locationId
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Initialize application session
        /// </summary>
        [HttpPost("session")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Initialize session", Description = "Initializes a new application session.")]
        [SwaggerResponse(200, "Session created", typeof(ApiResponse<WidgetSessionDto>))]
        public async Task<IActionResult> InitializeSession([FromBody] InitializeSessionDto sessionDto)
        {
            var command = new InitializeWidgetSessionCommand
            {
                CompanyId = sessionDto.CompanyId,
                LocationId = sessionDto.LocationId,
                Position = sessionDto.Position,
                SessionId = sessionDto.SessionId
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Validate session
        /// </summary>
        [HttpGet("session/{sessionId}/validate")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Validate session", Description = "Validates a widget session.")]
        [SwaggerResponse(200, "Session valid", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ValidateSession(string sessionId)
        {
            var query = new ValidateWidgetSessionQuery { SessionId = sessionId };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get application form fields
        /// </summary>
        [HttpGet("fields/{companyId}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Get form fields", Description = "Returns application form fields for a company.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<WidgetFieldDto>>))]
        public async Task<IActionResult> GetFormFields(int companyId, [FromQuery] int? locationId = null)
        {
            var query = new GetWidgetFormFieldsQuery
            {
                CompanyId = companyId,
                LocationId = locationId
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Submit application via widget
        /// </summary>
        [HttpPost("submit")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Submit via widget", Description = "Submits an application via the widget.")]
        [SwaggerResponse(201, "Application submitted", typeof(ApiResponse<WidgetApplicationResponseDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> SubmitViaWidget([FromBody] WidgetApplicationDto applicationDto)
        {
            applicationDto.IpAddress = GetIpAddress();
            applicationDto.UserAgent = GetUserAgent();

            var command = new SubmitWidgetApplicationCommand { Application = applicationDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return Created("", result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Track widget event
        /// </summary>
        [HttpPost("track")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Track event", Description = "Tracks widget events for analytics.")]
        [SwaggerResponse(200, "Event tracked", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> TrackEvent([FromBody] WidgetEventDto eventDto)
        {
            var command = new TrackWidgetEventCommand
            {
                SessionId = eventDto.SessionId,
                EventType = eventDto.EventType,
                EventData = eventDto.EventData
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }
    }

    public class InitializeSessionDto
    {
        public int CompanyId { get; set; }
        public int? LocationId { get; set; }
        public string Position { get; set; }
        public string SessionId { get; set; }
    }

    public class WidgetEventDto
    {
        public string SessionId { get; set; }
        public string EventType { get; set; }
        public Dictionary<string, object> EventData { get; set; } = new();
    }
}