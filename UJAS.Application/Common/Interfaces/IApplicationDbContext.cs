using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Assessment;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Field;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Entities.System;
using UJAS.Core.Entities.User;

namespace UJAS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<tUser> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Core.Entities.User.UserRole> UserRoles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<RolePermission> RolePermissions { get; }
        DbSet<tCompany> Companies { get; }
        DbSet<Location> Locations { get; }
        DbSet<CompanySettings> CompanySettings { get; }
        DbSet<CompanyUser> CompanyUsers { get; }
        DbSet<RegionalManagerLocation> RegionalManagerLocations { get; }
        DbSet<ApplicantProfile> ApplicantProfiles { get; }
        DbSet<EducationHistory> EducationHistories { get; }
        DbSet<WorkExperience> WorkExperiences { get; }
        DbSet<Skill> Skills { get; }
        DbSet<Document> Documents { get; }
        DbSet<Reference> References { get; }
        DbSet<EmergencyContact> EmergencyContacts { get; }
        DbSet<tApplication> Applications { get; }
        DbSet<ApplicationAnswer> ApplicationAnswers { get; }
        DbSet<ApplicationComment> ApplicationComments { get; }
        DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; }
        DbSet<ApplicationDocument> ApplicationDocuments { get; }
        DbSet<SystemField> SystemFields { get; }
        DbSet<CompanyField> CompanyFields { get; }
        DbSet<LocationField> LocationFields { get; }
        DbSet<tAssessment> Assessments { get; }
        DbSet<AssessmentQuestion> AssessmentQuestions { get; }
        DbSet<ApplicationAssessment> ApplicationAssessments { get; }
        DbSet<AssessmentResponse> AssessmentResponses { get; }
        DbSet<AuditLog> AuditLogs { get; }
        DbSet<SystemSettings> SystemSettings { get; }
        DbSet<EmailTemplate> EmailTemplates { get; }
        DbSet<Notification> Notifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<T> Set<T>() where T : class;
    }
}
