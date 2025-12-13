using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UJAS.Application.Users.DTOs;
using UJAS.Core.Entities.User;
using UJAS.Infrastructure.Data;

namespace UJAS.Application.Users.Commands
{
    public class LoginCommand : IRequest<ApiResponse<AuthResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
    {
        private readonly UserManager<tUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public LoginCommandHandler(
            UserManager<tUser> userManager,
            IJwtService jwtService,
            IMapper mapper,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = string.Empty,
                        ErrorMessage = "Invalid email or password"
                    }
                });

            // Check if email is confirmed
            if (!user.EmailConfirmed)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.Email),
                        ErrorMessage = "Please confirm your email address before logging in"
                    }
                });

            // Check if account is locked
            if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = string.Empty,
                        ErrorMessage = $"Account is locked until {user.LockoutEnd.Value:g}"
                    }
                });

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            // Generate JWT token
            var token = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(request.RememberMe ? 30 : 7);
            await _userManager.UpdateAsync(user);

            var authResponse = new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpiration = DateTime.UtcNow.AddHours(1),
                User = _mapper.Map<UserDto>(user),
                Permissions = await GetUserPermissionsAsync(user)
            };

            return ApiResponse<AuthResponseDto>.SuccessResult(authResponse, "Login successful");
        }

        private async Task<List<string>> GetUserPermissionsAsync(tUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = await _context.RolePermissions
                .Include(rp => rp.Permission)
                .Where(rp => roles.Contains(rp.Role.Name))
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToListAsync();

            return permissions;
        }
    }
}