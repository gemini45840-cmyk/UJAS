using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Application.Common.DTOs;
using UJAS.Application.Common.Interfaces;
using UJAS.Application.Profiles.Dtos;
using UJAS.Core.Entities.Profile;
using UJAS.Infrastructure.Data;

namespace UJAS.Application.Profiles.Commands
{
    public class UpdateProfileCommand : IRequest<ApiResponse<ApplicantProfileDto>>
    {
        // Personal Information
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredFirstName { get; set; }
        public string AlternateEmail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PreferredContactMethod { get; set; }
        public string BestTimeToContact { get; set; }

        // Address
        public AddressDto CurrentAddress { get; set; }
        public bool SameAsMailingAddress { get; set; }
        public AddressDto MailingAddress { get; set; }
        public int? YearsAtCurrentAddress { get; set; }
        public string PreviousAddress { get; set; }

        // Demographic Information
        public string GenderIdentity { get; set; }
        public string GenderSelfDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Ethnicity { get; set; }
        public string VeteranStatus { get; set; }
        public string DisabilityStatus { get; set; }
        public string WorkAuthorization { get; set; }
        public string VisaType { get; set; }
        public DateTime? VisaExpirationDate { get; set; }

        // Employment Preferences
        public string DesiredJobTitle { get; set; }
        public string EmploymentTypeDesired { get; set; }
        public string WorkSchedulePreference { get; set; }
        public string ShiftAvailability { get; set; }
        public bool? WillingToWorkOvertime { get; set; }
        public decimal? MinimumAcceptableSalary { get; set; }
        public SalaryRangeDto DesiredSalaryRange { get; set; }
        public bool? WillingToRelocate { get; set; }
        public int? RelocationRadius { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public int? NoticePeriodDays { get; set; }
        public string ReferredByEmployee { get; set; }
        public string HowDidYouHear { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ApiResponse<ApplicantProfileDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public UpdateProfileCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public async Task<ApiResponse<ApplicantProfileDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _context.ApplicantProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == _currentUserService.UserId, cancellationToken);

            if (profile == null)
                throw new NotFoundException(nameof(ApplicantProfile), _currentUserService.UserId);

            // Update personal information
            profile.Salutation = request.Salutation != null ?
                Enum.Parse<Salutation>(request.Salutation) : profile.Salutation;
            profile.FirstName = request.FirstName ?? profile.FirstName;
            profile.MiddleName = request.MiddleName ?? profile.MiddleName;
            profile.LastName = request.LastName ?? profile.LastName;
            profile.PreferredFirstName = request.PreferredFirstName ?? profile.PreferredFirstName;
            profile.AlternateEmail = request.AlternateEmail ?? profile.AlternateEmail;
            profile.MobilePhone = request.MobilePhone ?? profile.MobilePhone;
            profile.HomePhone = request.HomePhone ?? profile.HomePhone;
            profile.WorkPhone = request.WorkPhone ?? profile.WorkPhone;

            if (request.PreferredContactMethod != null)
                profile.PreferredContactMethod = Enum.Parse<ContactMethod>(request.PreferredContactMethod);

            if (request.BestTimeToContact != null)
                profile.BestTimeToContact = Enum.Parse<ContactTime>(request.BestTimeToContact);

            // Update address
            if (request.CurrentAddress != null)
            {
                profile.AddressLine1 = request.CurrentAddress.Line1;
                profile.AddressLine2 = request.CurrentAddress.Line2;
                profile.City = request.CurrentAddress.City;
                profile.StateProvince = request.CurrentAddress.StateProvince;
                profile.ZipPostalCode = request.CurrentAddress.ZipPostalCode;
                profile.Country = request.CurrentAddress.Country;

                if (request.CurrentAddress.AddressType != null)
                    profile.AddressType = Enum.Parse<AddressType>(request.CurrentAddress.AddressType);
            }

            profile.SameAsMailingAddress = request.SameAsMailingAddress;
            profile.YearsAtCurrentAddress = request.YearsAtCurrentAddress ?? profile.YearsAtCurrentAddress;
            profile.PreviousAddress = request.PreviousAddress ?? profile.PreviousAddress;

            // Update demographic information
            if (request.GenderIdentity != null)
                profile.GenderIdentity = Enum.Parse<GenderIdentity>(request.GenderIdentity);

            profile.GenderSelfDescription = request.GenderSelfDescription ?? profile.GenderSelfDescription;
            profile.DateOfBirth = request.DateOfBirth ?? profile.DateOfBirth;

            if (request.Ethnicity != null)
                profile.Ethnicity = Enum.Parse<Ethnicity>(request.Ethnicity);

            if (request.VeteranStatus != null)
                profile.VeteranStatus = Enum.Parse<VeteranStatus>(request.VeteranStatus);

            if (request.DisabilityStatus != null)
                profile.DisabilityStatus = Enum.Parse<DisabilityStatus>(request.DisabilityStatus);

            if (request.WorkAuthorization != null)
                profile.WorkAuthorization = Enum.Parse<WorkAuthorizationType>(request.WorkAuthorization);

            profile.VisaType = request.VisaType ?? profile.VisaType;
            profile.VisaExpirationDate = request.VisaExpirationDate ?? profile.VisaExpirationDate;

            // Update employment preferences
            profile.DesiredJobTitle = request.DesiredJobTitle ?? profile.DesiredJobTitle;

            if (request.EmploymentTypeDesired != null)
                profile.EmploymentTypeDesired = Enum.Parse<EmploymentType>(request.EmploymentTypeDesired);

            if (request.WorkSchedulePreference != null)
                profile.WorkSchedulePreference = Enum.Parse<WorkSchedule>(request.WorkSchedulePreference);

            if (request.ShiftAvailability != null)
                profile.ShiftAvailability = Enum.Parse<ShiftAvailability>(request.ShiftAvailability);

            profile.WillingToWorkOvertime = request.WillingToWorkOvertime ?? profile.WillingToWorkOvertime;
            profile.MinimumAcceptableSalary = request.MinimumAcceptableSalary ?? profile.MinimumAcceptableSalary;
            profile.DesiredSalaryFrom = request.DesiredSalaryRange?.Minimum ?? profile.DesiredSalaryFrom;
            profile.DesiredSalaryTo = request.DesiredSalaryRange?.Maximum ?? profile.DesiredSalaryTo;
            profile.WillingToRelocate = request.WillingToRelocate ?? profile.WillingToRelocate;
            profile.RelocationRadius = request.RelocationRadius ?? profile.RelocationRadius;
            profile.AvailableStartDate = request.AvailableStartDate ?? profile.AvailableStartDate;
            profile.NoticePeriodDays = request.NoticePeriodDays ?? profile.NoticePeriodDays;
            profile.ReferredByEmployee = request.ReferredByEmployee ?? profile.ReferredByEmployee;
            profile.HowDidYouHear = request.HowDidYouHear ?? profile.HowDidYouHear;

            profile.UpdatedAt = _dateTime.UtcNow;

            // Also update user's basic info
            if (!string.IsNullOrEmpty(request.FirstName))
                profile.User.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                profile.User.LastName = request.LastName;

            if (!string.IsNullOrEmpty(request.MobilePhone))
                profile.User.PhoneNumber = request.MobilePhone;

            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ApplicantProfileDto>(profile);
            return ApiResponse<ApplicantProfileDto>.SuccessResult(result, "Profile updated successfully");
        }
    }
}
