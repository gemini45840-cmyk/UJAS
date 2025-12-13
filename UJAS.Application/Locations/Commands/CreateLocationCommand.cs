using System;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UJAS.Application.Common.Base;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Common.Models;
using UJAS.Application.Locations.Dtos;
using UJAS.Infrastructure.Repositories.Base;

namespace UJAS.Application.Locations.Commands
{
    public class CreateLocationCommand : BaseCommand<LocationDto>
    {
        public CreateLocationDto Location { get; set; }
    }

    public class CreateLocationCommandHandler : BaseHandler, IRequestHandler<CreateLocationCommand, ApiResponse<LocationDto>>
    {
        public CreateLocationCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUser,
            IDateTimeService dateTime,
            ILogger<CreateLocationCommandHandler> logger)
            : base(unitOfWork, mapper, currentUser, dateTime, logger)
        {
        }

        public async Task<ApiResponse<LocationDto>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check access - only system admins and company admins can create locations
                if (!await ValidateUserAccessAsync(request.Location.CompanyId))
                    return await HandleUnauthorizedAsync<LocationDto>("create location");

                // Validate company exists
                var company = await _unitOfWork.Repository<Core.Entities.Company.tCompany>()
                    .GetByIdAsync(request.Location.CompanyId);

                if (company == null)
                    return ApiResponse<LocationDto>.FailureResponse("Company not found");

                // Check if location code is unique within company
                var existingLocation = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                    .GetSingleAsync(l => l.CompanyId == request.Location.CompanyId &&
                                        l.Code == request.Location.Code &&
                                        !l.IsDeleted);

                if (existingLocation != null)
                    return ApiResponse<LocationDto>.FailureResponse(
                        "A location with this code already exists in this company");

                // If this is set as headquarters, update existing headquarters
                if (request.Location.IsHeadquarters)
                {
                    var currentHeadquarters = await _unitOfWork.Repository<Core.Entities.Company.Location>()
                        .GetSingleAsync(l => l.CompanyId == request.Location.CompanyId &&
                                            l.IsHeadquarters &&
                                            !l.IsDeleted);

                    if (currentHeadquarters != null)
                    {
                        currentHeadquarters.IsHeadquarters = false;
                        await _unitOfWork.Repository<Core.Entities.Company.Location>().UpdateAsync(currentHeadquarters);
                    }
                }

                // Create location
                var location = _mapper.Map<Core.Entities.Company.Location>(request.Location);
                location.CreatedAt = _dateTime.UtcNow;
                location.CreatedBy = _currentUser.Email ?? "system";
                location.IsActive = true;

                await _unitOfWork.Repository<Core.Entities.Company.Location>().AddAsync(location);
                await _unitOfWork.SaveChangesAsync();

                var locationDto = _mapper.Map<LocationDto>(location);
                locationDto.CompanyName = company.Name;

                return ApiResponse<LocationDto>.SuccessResponse(locationDto, "Location created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating location");
                return ApiResponse<LocationDto>.FailureResponse("Error creating location");
            }
        }
    }
}