using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UJAS.Application.Users.DTOs;
using UJAS.Core.Entities.User;
using UJAS.Infrastructure.Data;
using UJAS.Infrastructure.Services.Email;

namespace UJAS.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new();
        public bool SendWelcomeEmail { get; set; } = true;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<tUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateUserCommandHandler(
            IApplicationDbContext context,
            UserManager<tUser> userManager,
            IMapper mapper,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ApiResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
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

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new ValidationException(result.Errors.Select(e =>
                    new ValidationError { PropertyName = e.Code, ErrorMessage = e.Description }).ToList());

            // Assign roles
            if (request.Roles.Any())
            {
                var validRoles = await _context.Roles
                    .Where(r => request.Roles.Contains(r.Name))
                    .Select(r => r.Name)
                    .ToListAsync(cancellationToken);

                if (validRoles.Any())
                {
                    await _userManager.AddToRolesAsync(user, validRoles);
                }
            }

            // Send welcome email
            if (request.SendWelcomeEmail)
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendWelcomeEmailAsync(user.Email, user.FirstName, emailToken);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse<UserDto>.SuccessResult(userDto, "User created successfully");
        }
    }
}