using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UJAS.Application.Common.Models;

namespace UJAS.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                var errorMessages = errors
                    .SelectMany(e => e.Value)
                    .ToList();

                var response = ApiResponse.FailureResponse(
                    "Validation failed",
                    errorMessages,
                    StatusCodes.Status400BadRequest
                );

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}