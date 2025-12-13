using UJAS.Application.Applications.Dtos;
using UJAS.Application.Companies.Dtos;
using UJAS.Application.Locations.Dtos;
using UJAS.Application.Profiles.Dtos;

namespace UJAS.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Company mappings
            CreateMap<Core.Entities.Company.tCompany, CompanyDto>();
            CreateMap<CompanyDto, Core.Entities.Company.tCompany>();
            CreateMap<CreateCompanyDto, Core.Entities.Company.tCompany>();
            CreateMap<UpdateCompanyDto, Core.Entities.Company.tCompany>();

            CreateMap<Core.Entities.Company.CompanySettings, CompanySettingsDto>();
            CreateMap<CompanySettingsDto, Core.Entities.Company.CompanySettings>();

            // Location mappings
            CreateMap<Core.Entities.Company.Location, LocationDto>();
            CreateMap<LocationDto, Core.Entities.Company.Location>();
            CreateMap<CreateLocationDto, Core.Entities.Company.Location>();
            CreateMap<UpdateLocationDto, Core.Entities.Company.Location>();

            // Profile mappings
            CreateMap<Core.Entities.Profile.ApplicantProfile, ApplicantProfileDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth.HasValue ?
                    (int?)((DateTime.Now - src.DateOfBirth.Value).TotalDays / 365.25) : null));

            CreateMap<ApplicantProfileDto, Core.Entities.Profile.ApplicantProfile>();
            CreateMap<UpdateApplicantProfileDto, Core.Entities.Profile.ApplicantProfile>();

            CreateMap<Core.Entities.Profile.EducationHistory, EducationHistoryDto>();
            CreateMap<EducationHistoryDto, Core.Entities.Profile.EducationHistory>();
            CreateMap<CreateEducationHistoryDto, Core.Entities.Profile.EducationHistory>();
            CreateMap<UpdateEducationHistoryDto, Core.Entities.Profile.EducationHistory>();

            CreateMap<Core.Entities.Profile.WorkExperience, WorkExperienceDto>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                    src.EndDate.HasValue ?
                    $"{((src.EndDate.Value.Year - src.StartDate.Year) * 12 + src.EndDate.Value.Month - src.StartDate.Month)} months" :
                    $"Since {src.StartDate:MMM yyyy}"));

            CreateMap<WorkExperienceDto, Core.Entities.Profile.WorkExperience>();
            CreateMap<CreateWorkExperienceDto, Core.Entities.Profile.WorkExperience>();
            CreateMap<UpdateWorkExperienceDto, Core.Entities.Profile.WorkExperience>();

            CreateMap<Core.Entities.Profile.Skill, SkillDto>();
            CreateMap<SkillDto, Core.Entities.Profile.Skill>();
            CreateMap<CreateSkillDto, Core.Entities.Profile.Skill>();
            CreateMap<UpdateSkillDto, Core.Entities.Profile.Skill>();

            // Application mappings
            CreateMap<Core.Entities.Application.tApplication, ApplicationDto>()
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src =>
                    $"{src.ApplicantProfile.FirstName} {src.ApplicantProfile.LastName}"))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));

            CreateMap<ApplicationDto, Core.Entities.Application.tApplication>();
            CreateMap<CreateApplicationDto, Core.Entities.Application.tApplication>();
            CreateMap<UpdateApplicationDto, Core.Entities.Application.tApplication>();

            CreateMap<Core.Entities.Application.ApplicationStatusHistory, ApplicationStatusHistoryDto>();
            CreateMap<ApplicationStatusHistoryDto, Core.Entities.Application.ApplicationStatusHistory>();

            CreateMap<Core.Entities.Application.ApplicationComment, ApplicationCommentDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                    $"{src.User.FirstName} {src.User.LastName}"));

            // Assessment mappings
            CreateMap<Core.Entities.Assessment.tAssessment, AssessmentDto>();
            CreateMap<AssessmentDto, Core.Entities.Assessment.tAssessment>();
            CreateMap<CreateAssessmentDto, Core.Entities.Assessment.tAssessment>();
            CreateMap<UpdateAssessmentDto, Core.Entities.Assessment.tAssessment>();

            CreateMap<Core.Entities.Assessment.ApplicationAssessment, ApplicationAssessmentDto>();
            CreateMap<ApplicationAssessmentDto, Core.Entities.Assessment.ApplicationAssessment>();

            // Field mappings
            CreateMap<Core.Entities.Field.SystemField, SystemFieldDto>();
            CreateMap<SystemFieldDto, Core.Entities.Field.SystemField>();

            CreateMap<Core.Entities.Field.CompanyField, CompanyFieldDto>();
            CreateMap<CompanyFieldDto, Core.Entities.Field.CompanyField>();
            CreateMap<CreateCompanyFieldDto, Core.Entities.Field.CompanyField>();
            CreateMap<UpdateCompanyFieldDto, Core.Entities.Field.CompanyField>();

            // User mappings
            CreateMap<Core.Entities.User.tUser, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserDto, Core.Entities.User.tUser>();
            CreateMap<CreateUserDto, Core.Entities.User.tUser>();
            CreateMap<UpdateUserDto, Core.Entities.User.tUser>();

            CreateMap<Core.Entities.User.CompanyUser, CompanyUserDto>();
            CreateMap<CompanyUserDto, Core.Entities.User.CompanyUser>();

            // Notification mappings
            CreateMap<Core.Entities.System.Notification, NotificationDto>();
            CreateMap<NotificationDto, Core.Entities.System.Notification>();

            // Value object mappings
            CreateMap<Core.ValueObjects.Address, AddressDto>();
            CreateMap<AddressDto, Core.ValueObjects.Address>();

            CreateMap<Core.ValueObjects.PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumberDto, Core.ValueObjects.PhoneNumber>();

            CreateMap<Core.ValueObjects.SalaryRange, SalaryRangeDto>();
            CreateMap<SalaryRangeDto, Core.ValueObjects.SalaryRange>();
        }
    }

    // Extension method for paginated mapping
    public static class MappingExtensions
    {
        public static PaginatedResponse<TDestination> ToPaginatedResponse<TSource, TDestination>(
            this IMapper mapper,
            List<TSource> source,
            int count,
            int pageNumber,
            int pageSize)
        {
            var items = mapper.Map<List<TDestination>>(source);
            return new PaginatedResponse<TDestination>(items, count, pageNumber, pageSize);
        }

        public static Task<PaginatedResponse<TDestination>> ToPaginatedResponseAsync<TSource, TDestination>(
            this IMapper mapper,
            List<TSource> source,
            int count,
            int pageNumber,
            int pageSize)
        {
            return Task.FromResult(mapper.ToPaginatedResponse<TSource, TDestination>(source, count, pageNumber, pageSize));
        }
    }
}