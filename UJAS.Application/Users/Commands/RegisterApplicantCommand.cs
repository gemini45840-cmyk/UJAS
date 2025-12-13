using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UJAS.Application.Users.DTOs;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Entities.User;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.Email;

namespace UJAS.Application.Users.Commands
{
    public class RegisterApplicantCommand : IRequest<ApiResponse<AuthResponseDto>>
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public bool AcceptTerms { get; set; }
        public string ReferralSource { get; set; }
    }

    public class RegisterApplicantCommandHandler : IRequestHandler<RegisterApplicantCommand, ApiResponse<AuthResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<tUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public RegisterApplicantCommandHandler(
            IApplicationDbContext context,
            UserManager<tUser> userManager,
            IJwtService jwtService,
            IMapper mapper,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ApiResponse<AuthResponseDto>> Handle(RegisterApplicantCommand request, CancellationToken cancellationToken)
        {
            // Validate terms acceptance
            if (!request.AcceptTerms)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.AcceptTerms),
                        ErrorMessage = "You must accept the terms and conditions"
                    }
                });

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.Email),
                        ErrorMessage = "User with this email already exists"
                    }
                });

            // Create user
            var user = new tUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                throw new ValidationException(createResult.Errors.Select(e =>
                    new ValidationError { PropertyName = e.Code, ErrorMessage = e.Description }).ToList());

            // Assign Applicant role
            var applicantRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Applicant", cancellationToken);
            if (applicantRole != null)
            {
                await _userManager.AddToRoleAsync(user, applicantRole.Name);
            }

            // Create applicant profile
            var profile = new ApplicantProfile
            {
                UserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                MobilePhone = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApplicantProfiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);

            // Generate email confirmation token
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Send welcome email
            await _emailService.SendWelcomeEmailAsync(user.Email, user.FirstName, emailToken);

            // Generate JWT token
            var token = await _jwtService.GenerateTokenAsync(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            var authResponse = new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                TokenExpiration = DateTime.UtcNow.AddHours(1),
                User = _mapper.Map<UserDto>(user),
                Permissions = await GetUserPermissionsAsync(user)
            };

            return ApiResponse<AuthResponseDto>.SuccessResult(authResponse, "Registration successful");
        }

        private async Task<List<string>> GetUserPermissionsAsync(User user)
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

