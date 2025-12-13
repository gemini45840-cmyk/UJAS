using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Applications.Commands;
using UJAS.Application.Applications.Dtos;
using UJAS.Application.Applications.Queries;
using UJAS.Application.Common.Models;
using UJAS.Core.Enums;

namespace UJAS.API.Controllers.Applications
{
    [ApiVersion("1.0")]
    public class ApplicationsController : BaseApiController
    {
        /// <summary>
        /// Get all applications with filtering
        /// </summary>
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Get applications", Description = "Returns applications based on user role and filters.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<PaginatedResponse<ApplicationDto>>))]
        public async Task<IActionResult> GetApplications([FromQuery] GetApplicationsQuery query)
        {
            var result = await Mediator.Send(query);
            return PaginatedResult(result.Data);
        }

        /// <summary>
        /// Get application by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get application by ID", Description = "Returns application details by ID.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ApplicationDto>))]
        [SwaggerResponse(404, "Application not found")]
        public async Task<IActionResult> GetApplication(int id,
            [FromQuery] bool includeHistory = false,
            [FromQuery] bool includeComments = false,
            [FromQuery] bool includeAssessments = false)
        {
            var query = new GetApplicationByIdQuery
            {
                ApplicationId = id,
                IncludeHistory = includeHistory,
                IncludeComments = includeComments,
                IncludeAssessments = includeAssessments
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Submit a new application
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Submit application", Description = "Submits a new job application.")]
        [SwaggerResponse(201, "Application submitted", typeof(ApiResponse<ApplicationDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> SubmitApplication([FromBody] CreateApplicationDto applicationDto)
        {
            // Add tracking info
            applicationDto.IpAddress = GetIpAddress();
            applicationDto.UserAgent = GetUserAgent();

            var command = new SubmitApplicationCommand { Application = applicationDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetApplication), new { id = result.Data.Id }, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update application status
        /// </summary>
        [HttpPut("{id}/status")]
        [Authorize(Policy = "ManagerOrAbove")]
        [SwaggerOperation(Summary = "Update application status", Description = "Updates the status of an application.")]
        [SwaggerResponse(200, "Status updated", typeof(ApiResponse<ApplicationDto>))]
        [SwaggerResponse(404, "Application not found")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateApplicationStatusDto statusDto)
        {
            var command = new UpdateApplicationStatusCommand
            {
                ApplicationId = id,
                Status = statusDto.Status,
                Reason = statusDto.Reason,
                Notes = statusDto.Notes
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Withdraw application
        /// </summary>
        [HttpPost("{id}/withdraw")]
        [Authorize(Policy = "ApplicantOnly")]
        [SwaggerOperation(Summary = "Withdraw application", Description = "Withdraws an application (applicant only).")]
        [SwaggerResponse(200, "Application withdrawn", typeof(ApiResponse<ApplicationDto>))]
        public async Task<IActionResult> WithdrawApplication(int id, [FromBody] WithdrawApplicationDto withdrawDto)
        {
            var command = new WithdrawApplicationCommand
            {
                ApplicationId = id,
                Reason = withdrawDto.Reason
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Add comment to application
        /// </summary>
        [HttpPost("{id}/comments")]
        [Authorize(Policy = "ManagerOrAbove")]
        [SwaggerOperation(Summary = "Add comment", Description = "Adds a comment to an application.")]
        [SwaggerResponse(201, "Comment added", typeof(ApiResponse<ApplicationCommentDto>))]
        public async Task<IActionResult> AddComment(int id, [FromBody] AddCommentDto commentDto)
        {
            var command = new AddApplicationCommentCommand
            {
                ApplicationId = id,
                CommentText = commentDto.CommentText,
                IsInternal = commentDto.IsInternal,
                VisibleToApplicant = commentDto.VisibleToApplicant
            };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetApplication), new { id }, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get application dashboard
        /// </summary>
        [HttpGet("dashboard")]
        [Authorize]
        [SwaggerOperation(Summary = "Get dashboard", Description = "Returns application dashboard data.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<ApplicationDashboardDto>))]
        public async Task<IActionResult> GetDashboard([FromQuery] ApplicationDashboardQuery query)
        {
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Export applications
        /// </summary>
        [HttpGet("export")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Export applications", Description = "Exports applications to Excel/CSV.")]
        [SwaggerResponse(200, "Export file")]
        public async Task<IActionResult> ExportApplications([FromQuery] ExportApplicationsQuery query)
        {
            var result = await Mediator.Send(query);

            if (result.Success && result.Data != null)
            {
                var fileName = $"applications_{DateTime.UtcNow:yyyyMMdd_HHmmss}.{result.Data.Format}";
                return File(result.Data.Content, result.Data.ContentType, fileName);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Bulk update application status
        /// </summary>
        [HttpPost("bulk/status")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Bulk update status", Description = "Updates status for multiple applications.")]
        [SwaggerResponse(200, "Bulk update completed", typeof(ApiResponse<BulkUpdateResultDto>))]
        public async Task<IActionResult> BulkUpdateStatus([FromBody] BulkUpdateStatusCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }
    }

    public class UpdateApplicationStatusDto
    {
        public ApplicationStatus Status { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
    }

    public class WithdrawApplicationDto
    {
        public string Reason { get; set; }
    }

    public class AddCommentDto
    {
        public string CommentText { get; set; }
        public bool IsInternal { get; set; }
        public bool VisibleToApplicant { get; set; }
    }
}