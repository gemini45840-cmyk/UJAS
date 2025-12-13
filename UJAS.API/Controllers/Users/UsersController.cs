using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UJAS.API.Controllers.Base;
using UJAS.Application.Common.Models;
using UJAS.Application.Users.Commands;
using UJAS.Application.Users.Dtos;
using UJAS.Application.Users.Queries;

namespace UJAS.API.Controllers.Users
{
    [ApiVersion("1.0")]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register user", Description = "Registers a new user account.")]
        [SwaggerResponse(201, "User registered", typeof(ApiResponse<AuthResponseDto>))]
        [SwaggerResponse(400, "Validation error")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var command = new RegisterUserCommand
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Role = registerDto.Role
            };
            var result = await Mediator.Send(command);

            if (result.Success && result.Data != null)
            {
                return Created("", result);
            }

            return HandleApiResponse(result);
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Login user", Description = "Authenticates user and returns JWT token.")]
        [SwaggerResponse(200, "Login successful", typeof(ApiResponse<AuthResponseDto>))]
        [SwaggerResponse(401, "Invalid credentials")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            var command = new LoginUserCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Refresh token", Description = "Refreshes JWT token using refresh token.")]
        [SwaggerResponse(200, "Token refreshed", typeof(ApiResponse<AuthResponseDto>))]
        [SwaggerResponse(401, "Invalid refresh token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshDto)
        {
            var command = new RefreshTokenCommand { RefreshToken = refreshDto.RefreshToken };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get current user info
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(Summary = "Get current user", Description = "Returns current user information.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<UserDto>))]
        public async Task<IActionResult> GetCurrentUser()
        {
            var query = new GetCurrentUserQuery();
            var result = await Mediator.Send(query);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Update current user
        /// </summary>
        [HttpPut("me")]
        [Authorize]
        [SwaggerOperation(Summary = "Update current user", Description = "Updates current user information.")]
        [SwaggerResponse(200, "User updated", typeof(ApiResponse<UserDto>))]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto userDto)
        {
            var command = new UpdateCurrentUserCommand { User = userDto };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        [SwaggerOperation(Summary = "Change password", Description = "Changes current user's password.")]
        [SwaggerResponse(200, "Password changed", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var command = new ChangePasswordCommand
            {
                CurrentPassword = passwordDto.CurrentPassword,
                NewPassword = passwordDto.NewPassword
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Request password reset
        /// </summary>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Forgot password", Description = "Sends password reset email.")]
        [SwaggerResponse(200, "Reset email sent", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotDto)
        {
            var command = new ForgotPasswordCommand { Email = forgotDto.Email };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Reset password
        /// </summary>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Reset password", Description = "Resets password using token.")]
        [SwaggerResponse(200, "Password reset", typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetDto)
        {
            var command = new ResetPasswordCommand
            {
                Email = resetDto.Email,
                Token = resetDto.Token,
                NewPassword = resetDto.NewPassword
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }

        /// <summary>
        /// Get all users (System Admin only)
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Get all users", Description = "Returns all users. System administrators only.")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<PaginatedResponse<UserDto>>))]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            var result = await Mediator.Send(query);
            return PaginatedResult(result.Data);
        }

        /// <summary>
        /// Assign user to company
        /// </summary>
        [HttpPost("assign-company")]
        [Authorize(Policy = "SystemAdminOnly")]
        [SwaggerOperation(Summary = "Assign to company", Description = "Assigns a user to a company with a role.")]
        [SwaggerResponse(200, "User assigned", typeof(ApiResponse<CompanyUserDto>))]
        public async Task<IActionResult> AssignToCompany([FromBody] AssignUserToCompanyDto assignDto)
        {
            var command = new AssignUserToCompanyCommand
            {
                UserId = assignDto.UserId,
                CompanyId = assignDto.CompanyId,
                LocationId = assignDto.LocationId,
                IsCompanyAdmin = assignDto.IsCompanyAdmin,
                IsRegionalManager = assignDto.IsRegionalManager,
                IsManager = assignDto.IsManager
            };
            var result = await Mediator.Send(command);
            return HandleApiResponse(result);
        }
    }

    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    public class AssignUserToCompanyDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int? LocationId { get; set; }
        public bool IsCompanyAdmin { get; set; }
        public bool IsRegionalManager { get; set; }
        public bool IsManager { get; set; }
    }
}