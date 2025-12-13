using UJAS.Application.Common.Interfaces;
using UJAS.Application.Companies.Dtos;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.User;
using UJAS.Infrastructure.Data;

namespace UJAS.Application.Locations.Commands
{
    public class AssignCompanyUserCommand : IRequest<ApiResponse<CompanyUserDto>>
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int? LocationId { get; set; }
        public bool IsCompanyAdmin { get; set; }
        public bool IsRegionalManager { get; set; }
        public bool IsManager { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public List<int> RegionalLocationIds { get; set; } = new();
    }

    public class AssignCompanyUserCommandHandler : IRequestHandler<AssignCompanyUserCommand, ApiResponse<CompanyUserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AssignCompanyUserCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<CompanyUserDto>> Handle(AssignCompanyUserCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FindAsync(new object[] { request.CompanyId }, cancellationToken);
            if (company == null)
                throw new NotFoundException(nameof(tCompany), request.CompanyId);

            var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(tUser), request.UserId);

            // Check if user is already assigned to this company
            var existingAssignment = await _context.CompanyUsers
                .FirstOrDefaultAsync(cu => cu.CompanyId == request.CompanyId && cu.UserId == request.UserId, cancellationToken);

            if (existingAssignment != null)
                throw new ValidationException(new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = nameof(request.UserId),
                        ErrorMessage = "User is already assigned to this company"
                    }
                });

            var companyUser = new CompanyUser
            {
                CompanyId = request.CompanyId,
                UserId = request.UserId,
                LocationId = request.LocationId,
                IsCompanyAdmin = request.IsCompanyAdmin,
                IsRegionalManager = request.IsRegionalManager,
                IsManager = request.IsManager,
                EmployeeId = request.EmployeeId,
                Department = request.Department,
                Title = request.Title,
                CreatedBy = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.CompanyUsers.Add(companyUser);

            // Add regional manager locations if applicable
            if (request.IsRegionalManager && request.RegionalLocationIds.Any())
            {
                foreach (var locationId in request.RegionalLocationIds)
                {
                    var location = await _context.Locations
                        .FirstOrDefaultAsync(l => l.Id == locationId && l.CompanyId == request.CompanyId, cancellationToken);

                    if (location != null)
                    {
                        _context.RegionalManagerLocations.Add(new RegionalManagerLocation
                        {
                            CompanyUser = companyUser,
                            LocationId = locationId
                        });
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return ApiResponse<CompanyUserDto>.SuccessResult(
                _mapper.Map<CompanyUserDto>(companyUser),
                "User assigned to company successfully");
        }
    }
}