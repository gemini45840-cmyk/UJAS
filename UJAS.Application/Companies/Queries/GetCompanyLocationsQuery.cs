using UJAS.Application.Common.DTOs;
using UJAS.Application.Locations.Dtos;
using UJAS.Infrastructure.Data;

namespace UJAS.Application.Companies.Queries
{
    public class GetCompanyLocationsQuery : PaginationDto, IRequest<ApiResponse<PagedResultDto<LocationDto>>>
    {
        public int CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHeadquarters { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
    }

    public class GetCompanyLocationsQueryHandler : IRequestHandler<GetCompanyLocationsQuery, ApiResponse<PagedResultDto<LocationDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyLocationsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResultDto<LocationDto>>> Handle(GetCompanyLocationsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Locations
                .Include(l => l.Managers)
                .Include(l => l.Applications)
                .Where(l => l.CompanyId == request.CompanyId)
                .AsNoTracking();

            // Apply filters
            if (request.IsActive.HasValue)
            {
                query = query.Where(l => l.IsActive == request.IsActive.Value);
            }

            if (request.IsHeadquarters.HasValue)
            {
                query = query.Where(l => l.IsHeadquarters == request.IsHeadquarters.Value);
            }

            if (!string.IsNullOrEmpty(request.Country))
            {
                query = query.Where(l => l.Country == request.Country);
            }

            if (!string.IsNullOrEmpty(request.StateProvince))
            {
                query = query.Where(l => l.StateProvince == request.StateProvince);
            }

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending ?
                    query.OrderByDescending(l => l.Name) : query.OrderBy(l => l.Name),
                "city" => request.SortDescending ?
                    query.OrderByDescending(l => l.City) : query.OrderBy(l => l.City),
                _ => query.OrderBy(l => l.Name)
            };

            // Apply pagination
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new PagedResultDto<LocationDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return ApiResponse<PagedResultDto<LocationDto>>.SuccessResult(result);
        }
    }
}