using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UJAS.Core.Entities.User;
using UJAS.Core.Enums;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Infrastructure.Identity
{
    public interface IIdentityService
    {
        Task<(Result Result, string Token, tUser User)> LoginAsync(string email, string password);
        Task<(Result Result, string Token, tUser User)> RegisterAsync(tUser user, string password, Core.Entities.User.UserRole role);
        Task<Result> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<Result> ResetPasswordAsync(string email);
        Task<tUser> GetUserByIdAsync(int userId);
        Task<tUser> GetUserByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(tUser user);
        Task<bool> IsInRoleAsync(tUser user, string role);
        Task<Result> AddToRoleAsync(tUser user, string role);
        Task<Result> RemoveFromRoleAsync(tUser user, string role);
        Task<string> GenerateEmailConfirmationTokenAsync(tUser user);
        Task<Result> ConfirmEmailAsync(tUser user, string token);
        Task<string> GeneratePasswordResetTokenAsync(tUser user);
        Task<Result> ResetPasswordAsync(tUser user, string token, string newPassword);
    }

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<tUser> _userManager;
        private readonly SignInManager<tUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IRepository<CompanyUser> _companyUserRepository;

        public IdentityService(
            UserManager<tUser> userManager,
            SignInManager<Role> roleManager,
            SignInManager<tUser> signInManager,
            IConfiguration configuration,
            ILogger<IdentityService> logger,
            ApplicationDbContext context,
            IRepository<CompanyUser> companyUserRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _companyUserRepository = companyUserRepository;
        }

        public async Task<(Result Result, string Token, tUser User)> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return (Result.Failure("Invalid login attempt"), null, null);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                        return (Result.Failure("Account locked out"), null, null);
                    if (result.IsNotAllowed)
                        return (Result.Failure("Email not confirmed"), null, null);

                    return (Result.Failure("Invalid login attempt"), null, null);
                }

                var token = await GenerateJwtTokenAsync(user);
                return (Result.Success(), token, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return (Result.Failure("An error occurred during login"), null, null);
            }
        }

        public async Task<(Result Result, string Token, tUser User)> RegisterAsync(tUser user, string password, Core.Entities.User.UserRole role)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return (Result.Failure("Email already registered"), null, null);
                }

                user.UserName = user.Email;
                user.EmailConfirmed = true; // For now, auto-confirm email

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return (Result.Failure(errors), null, null);
                }

                // Add to role
                var roleName = role.ToString();
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.ToUpper() });
                }

                await _userManager.AddToRoleAsync(user, roleName);

                // Generate token
                var token = await GenerateJwtTokenAsync(user);
                return (Result.Success(), token, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return (Result.Failure("An error occurred during registration"), null, null);
            }
        }

        public async Task<tUser> GetUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<tUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(tUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRoleAsync(tUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> AddToRoleAsync(tUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded ? Result.Success() : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> RemoveFromRoleAsync(tUser user, string role)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded ? Result.Success() : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(tUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<Result> ConfirmEmailAsync(tUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded ? Result.Success() : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<string> GeneratePasswordResetTokenAsync(tUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<Result> ResetPasswordAsync(tUser user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded ? Result.Success() : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result.Failure("User not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded ? Result.Success() : Result.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<Result> ResetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user doesn't exist
                return Result.Success();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Send email with reset link (implement email service)
            return Result.Success();
        }

        private async Task<string> GenerateJwtTokenAsync(tUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Add company information if applicable
            var companyUser = await _companyUserRepository.GetSingleAsync(cu => cu.UserId == user.Id);
            if (companyUser != null)
            {
                claims.Add(new Claim("CompanyId", companyUser.CompanyId.ToString()));
                claims.Add(new Claim("LocationId", companyUser.LocationId?.ToString() ?? ""));
                claims.Add(new Claim("IsCompanyAdmin", companyUser.IsCompanyAdmin.ToString()));
                claims.Add(new Claim("IsRegionalManager", companyUser.IsRegionalManager.ToString()));
                claims.Add(new Claim("IsManager", companyUser.IsManager.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class Result
    {
        public bool Succeeded { get; }
        public string[] Errors { get; }

        protected Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }
    }
}