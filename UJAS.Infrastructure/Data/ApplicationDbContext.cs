using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UJAS.Core.Entities;
using UJAS.Core.Entities.Application;
using UJAS.Core.Entities.Assessment;
using UJAS.Core.Entities.Company;
using UJAS.Core.Entities.Field;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Entities.System;
using UJAS.Core.Entities.User;

namespace UJAS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<tUser, Role, int,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Company Entities
        public DbSet<tCompany> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }

        // Profile Entities
        public DbSet<ApplicantProfile> ApplicantProfiles { get; set; }
        public DbSet<EducationHistory> EducationHistories { get; set; }
        public DbSet<LicenseCertification> LicensesCertifications { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<MilitaryService> MilitaryServices { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<DriversLicenseInfo> DriversLicenseInfos { get; set; }
        public DbSet<CriminalHistory> CriminalHistories { get; set; }

        // Application Entities
        public DbSet<tApplication> Applications { get; set; }
        public DbSet<ApplicationAnswer> ApplicationAnswers { get; set; }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
        public DbSet<ApplicationComment> ApplicationComments { get; set; }
        public DbSet<ApplicationDocument> ApplicationDocuments { get; set; }

        // Field Entities
        public DbSet<SystemField> SystemFields { get; set; }
        public DbSet<CompanyField> CompanyFields { get; set; }
        public DbSet<LocationField> LocationFields { get; set; }

        // Assessment Entities
        public DbSet<tAssessment> Assessments { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public DbSet<ApplicationAssessment> ApplicationAssessments { get; set; }
        public DbSet<AssessmentResponse> AssessmentResponses { get; set; }

        // System Entities
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SystemSettings> SystemSettings { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // User Entities
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<RegionalManagerLocation> RegionalManagerLocations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from Configurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Configure decimal precision for all decimal properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetPrecision(18);
                        property.SetScale(2);
                    }
                }
            }

            // Configure soft delete filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(e => EF.Property<bool>(e, "IsDeleted") == false);
                }
            }

            // Configure Identity tables
            modelBuilder.Entity<tUser>(b =>
            {
                b.ToTable("Users");
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");
                b.HasIndex(u => u.NormalizedUserName).IsUnique().HasDatabaseName("UserNameIndex");
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("Roles");
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).IsUnique().HasDatabaseName("RoleNameIndex");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRoles");
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.ToTable("UserClaims");
                b.HasKey(uc => uc.Id);
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.ToTable("RoleClaims");
                b.HasKey(rc => rc.Id);
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.ToTable("UserLogins");
                b.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.ToTable("UserTokens");
                b.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            });

            // Configure composite keys
            ConfigureCompositeKeys(modelBuilder);
        }

        private void ConfigureCompositeKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationAnswer>()
                .HasIndex(a => new { a.ApplicationId, a.CompanyFieldId })
                .IsUnique();

            modelBuilder.Entity<AssessmentResponse>()
                .HasIndex(ar => new { ar.ApplicationAssessmentId, ar.QuestionId })
                .IsUnique();
        }

        public override int SaveChanges()
        {
            UpdateAuditableEntities();
            SoftDeleteEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();
            SoftDeleteEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentTime = DateTime.UtcNow;
            var currentUser = "system"; // Will be replaced with actual user from HttpContext

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = currentTime;
                    entity.CreatedBy = currentUser;
                    entity.IsActive = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = currentTime;
                    entity.UpdatedBy = currentUser;

                    // Don't modify CreatedAt and CreatedBy
                    Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                }
            }
        }

        private void SoftDeleteEntities()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}

public class RoleClaim : IdentityRoleClaim<int> { }