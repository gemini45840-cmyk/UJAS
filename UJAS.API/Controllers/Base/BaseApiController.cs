using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UJAS.Application.Common.Models;

namespace UJAS.API.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected IActionResult HandleApiResponse<T>(ApiResponse<T> response)
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                201 => Created("", response),
                204 => NoContent(),
                400 => BadRequest(response),
                401 => Unauthorized(response),
                403 => Forbid(),
                404 => NotFound(response),
                409 => Conflict(response),
                _ => StatusCode(response.StatusCode, response)
            };
        }

        protected IActionResult PaginatedResult<T>(PaginatedResponse<T> result)
        {
            Response.Headers.Add("X-Pagination-TotalCount", result.TotalCount.ToString());
            Response.Headers.Add("X-Pagination-PageSize", result.PageSize.ToString());
            Response.Headers.Add("X-Pagination-CurrentPage", result.PageNumber.ToString());
            Response.Headers.Add("X-Pagination-TotalPages", result.TotalPages.ToString());
            Response.Headers.Add("X-Pagination-HasPrevious", result.HasPreviousPage.ToString());
            Response.Headers.Add("X-Pagination-HasNext", result.HasNextPage.ToString());

            return Ok(ApiResponse<PaginatedResponse<T>>.SuccessResponse(result));
        }

        protected string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];

            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

        protected string GetUserAgent()
        {
            return Request.Headers["User-Agent"].ToString();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public abstract class PublicApiController : ControllerBase
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected IActionResult HandleApiResponse<T>(ApiResponse<T> response)
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                201 => Created("", response),
                400 => BadRequest(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                409 => Conflict(response),
                _ => StatusCode(response.StatusCode, response)
            };
        }
    }
}