using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Enums;

namespace UJAS.Infrastructure.Configurations.Profile
{
    public class ApplicantProfileConfiguration : IEntityTypeConfiguration<ApplicantProfile>
    {
        public void Configure(EntityTypeBuilder<ApplicantProfile> builder)
        {
            builder.ToTable("ApplicantProfiles");

            builder.HasKey(ap => ap.Id);

            // Personal Information
            builder.Property(ap => ap.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ap => ap.MiddleName)
                .HasMaxLength(100);

            builder.Property(ap => ap.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ap => ap.PreferredFirstName)
                .HasMaxLength(100);

            builder.Property(ap => ap.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(ap => ap.AlternateEmail)
                .HasMaxLength(256);

            builder.Property(ap => ap.MobilePhone)
                .HasMaxLength(20);

            builder.Property(ap => ap.HomePhone)
                .HasMaxLength(20);

            builder.Property(ap => ap.WorkPhone)
                .HasMaxLength(20);

            // Address
            builder.Property(ap => ap.AddressLine1)
                .HasMaxLength(200);

            builder.Property(ap => ap.AddressLine2)
                .HasMaxLength(200);

            builder.Property(ap => ap.City)
                .HasMaxLength(100);

            builder.Property(ap => ap.StateProvince)
                .HasMaxLength(100);

            builder.Property(ap => ap.ZipPostalCode)
                .HasMaxLength(20);

            builder.Property(ap => ap.Country)
                .HasMaxLength(100);

            // Employment Preferences
            builder.Property(ap => ap.DesiredJobTitle)
                .HasMaxLength(200);

            builder.Property(ap => ap.HowDidYouHear)
                .HasMaxLength(200);

            builder.Property(ap => ap.ReferredByEmployee)
                .HasMaxLength(100);

            builder.Property(ap => ap.VisaType)
                .HasMaxLength(50);

            // Resume
            builder.Property(ap => ap.ResumeFilePath)
                .HasMaxLength(500);

            builder.Property(ap => ap.PhotoUrl)
                .HasMaxLength(500);

            builder.Property(ap => ap.GenderSelfDescription)
                .HasMaxLength(100);

            builder.Property(ap => ap.FelonyExplanation)
                .HasMaxLength(2000);

            // Enums
            builder.Property(ap => ap.Salutation)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.PreferredContactMethod)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.BestTimeToContact)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.AddressType)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.GenderIdentity)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(ap => ap.Ethnicity)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(ap => ap.VeteranStatus)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(ap => ap.DisabilityStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.WorkAuthorization)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(ap => ap.EmploymentTypeDesired)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.WorkSchedulePreference)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.ShiftAvailability)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(ap => ap.ResumeVisibility)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Indexes
            builder.HasIndex(ap => ap.UserId).IsUnique();
            builder.HasIndex(ap => ap.Email);
            builder.HasIndex(ap => new { ap.FirstName, ap.LastName });
            builder.HasIndex(ap => ap.CreatedAt);

            // Relationships
            builder.HasMany(ap => ap.EducationHistories)
                .WithOne(eh => eh.ApplicantProfile)
                .HasForeignKey(eh => eh.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.LicensesCertifications)
                .WithOne(lc => lc.ApplicantProfile)
                .HasForeignKey(lc => lc.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.WorkExperiences)
                .WithOne(we => we.ApplicantProfile)
                .HasForeignKey(we => we.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.Skills)
                .WithOne(s => s.ApplicantProfile)
                .HasForeignKey(s => s.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.Documents)
                .WithOne(d => d.ApplicantProfile)
                .HasForeignKey(d => d.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.References)
                .WithOne(r => r.ApplicantProfile)
                .HasForeignKey(r => r.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.EmergencyContacts)
                .WithOne(ec => ec.ApplicantProfile)
                .HasForeignKey(ec => ec.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ap => ap.MilitaryService)
                .WithOne(ms => ms.ApplicantProfile)
                .HasForeignKey<MilitaryService>(ms => ms.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ap => ap.DriversLicenseInfo)
                .WithOne(dli => dli.ApplicantProfile)
                .HasForeignKey<DriversLicenseInfo>(dli => dli.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ap => ap.CriminalHistory)
                .WithOne(ch => ch.ApplicantProfile)
                .HasForeignKey<CriminalHistory>(ch => ch.ApplicantProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class EducationHistoryConfiguration : IEntityTypeConfiguration<EducationHistory>
    {
        public void Configure(EntityTypeBuilder<EducationHistory> builder)
        {
            builder.ToTable("EducationHistories");

            builder.HasKey(eh => eh.Id);

            builder.Property(eh => eh.InstitutionName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(eh => eh.City)
                .HasMaxLength(100);

            builder.Property(eh => eh.StateProvince)
                .HasMaxLength(100);

            builder.Property(eh => eh.Country)
                .HasMaxLength(100);

            builder.Property(eh => eh.FieldOfStudy)
                .HasMaxLength(200);

            builder.Property(eh => eh.MinorConcentration)
                .HasMaxLength(200);

            builder.Property(eh => eh.HonorsAwards)
                .HasMaxLength(500);

            builder.Property(eh => eh.RelevantCoursework)
                .HasMaxLength(2000);

            // Enums
            builder.Property(eh => eh.InstitutionType)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(eh => eh.DegreeCertificate)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Indexes
            builder.HasIndex(eh => eh.ApplicantProfileId);
            builder.HasIndex(eh => eh.InstitutionName);
            builder.HasIndex(eh => eh.GraduationDate);
        }
    }

    public class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> builder)
        {
            builder.ToTable("WorkExperiences");

            builder.HasKey(we => we.Id);

            builder.Property(we => we.EmployerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(we => we.EmployerCity)
                .HasMaxLength(100);

            builder.Property(we => we.EmployerStateProvince)
                .HasMaxLength(100);

            builder.Property(we => we.EmployerCountry)
                .HasMaxLength(100);

            builder.Property(we => we.JobTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(we => we.ReasonForLeaving)
                .HasMaxLength(500);

            builder.Property(we => we.SupervisorName)
                .HasMaxLength(100);

            builder.Property(we => we.SupervisorTitle)
                .HasMaxLength(100);

            builder.Property(we => we.SupervisorContact)
                .HasMaxLength(200);

            builder.Property(we => we.JobResponsibilities)
                .HasMaxLength(4000);

            builder.Property(we => we.KeyAccomplishments)
                .HasMaxLength(2000);

            builder.Property(we => we.SkillsUtilized)
                .HasMaxLength(1000);

            builder.Property(we => we.EquipmentSoftwareUsed)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(we => we.ApplicantProfileId);
            builder.HasIndex(we => we.EmployerName);
            builder.HasIndex(we => we.JobTitle);
            builder.HasIndex(we => new { we.StartDate, we.EndDate });
        }
    }

    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.FilePath)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(d => d.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.DocumentType)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Indexes
            builder.HasIndex(d => d.ApplicantProfileId);
            builder.HasIndex(d => d.DocumentType);
            builder.HasIndex(d => d.IsPrimary);
            builder.HasIndex(d => d.ExpirationDate);
        }
    }
}