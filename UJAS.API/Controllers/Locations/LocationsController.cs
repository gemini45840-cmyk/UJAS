using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Common.Models;
using UJAS.Application.Locations.Commands;
using UJAS.Application.Locations.Dtos;
using UJAS.Application.Locations.Queries;

namespace UJAS.API.Controllers.Locations
{
    [ApiVersion("1.0")]
    public class LocationsController : BaseApiController
    {
        /// <summary>
        /// Get all locations for a company
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Get company locations", Description = "Returns all locations for a company.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<PaginatedResponse<LocationDto>>))]
        public async Task<IActionResult> GetLocations([FromQuery] GetLocationsQuery query)
        {
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get location by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Get location by ID", Description = "Returns location details by ID.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LocationDto>))]
        [SwaggerResponse(404, "Location not found")]
        public async Task<IActionResult> GetLocation(int id, [FromQuery] bool includeManagers = false)
        {
            var query = new GetLocationByIdQuery
            {
                LocationId = id,
                IncludeManagers = includeManagers
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Create a new location
        /// </summary>
        [HttpPost]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Create location", Description = "Creates a new location for a company.")]
        [SwaggerResponse(201, "Location created", typeof(ApiResponse<LocationDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto locationDto)
        {
            var command = new CreateLocationCommand { Location = locationDto };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return CreatedAtAction(nameof(GetLocation), new { id = result.Data.Id }, result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update location
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Update location", Description = "Updates location details.")]
        [SwaggerResponse(200, "Location updated", typeof(ApiResponse<LocationDto>))]
        [SwaggerResponse(404, "Location not found")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateLocationDto locationDto)
        {
            var command = new UpdateLocationCommand
            {
                LocationId = id,
                Location = locationDto
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Delete location
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Delete location", Description = "Soft deletes a location.")]
        [SwaggerResponse(204, "Location deleted")]
        [SwaggerResponse(404, "Location not found")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var command = new DeleteLocationCommand { LocationId = id };
            var result = await Mediator.Send(command);

            if (result.Success)
                return NoContent();

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Assign manager to location
        /// </summary>
        [HttpPost("{id}/managers")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Assign manager", Description = "Assigns a manager to a location.")]
        [SwaggerResponse(200, "Manager assigned", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AssignManager(int id, [FromBody] AssignManagerDto assignDto)
        {
            var command = new AssignManagerCommand
            {
                LocationId = id,
                UserId = assignDto.UserId,
                IsPrimaryManager = assignDto.IsPrimaryManager
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Remove manager from location
        /// </summary>
        [HttpDelete("{id}/managers/{userId}")]
        [Authorize(Policy = "CompanyAdminOrSystemAdmin")]
        [SwaggerOperation(Summary = "Remove manager", Description = "Removes a manager from a location.")]
        [SwaggerResponse(200, "Manager removed", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> RemoveManager(int id, int userId)
        {
            var command = new RemoveManagerCommand
            {
                LocationId = id,
                UserId = userId
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get location analytics
        /// </summary>
        [HttpGet("{id}/analytics")]
        [Authorize(Policy = "CompanyAccess")]
        [SwaggerOperation(Summary = "Get location analytics", Description = "Returns analytics for a location.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LocationStatisticsDto>))]
        public async Task<IActionResult> GetAnalytics(int id, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetLocationAnalyticsQuery
            {
                LocationId = id,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }
    }
}