using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UJAS.Core.Entities.Application;

namespace UJAS.Infrastructure.Configurations.Application
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Applications");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ApplicationNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.PositionAppliedFor)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.RejectionReason)
                .HasMaxLength(1000);

            builder.Property(a => a.WithdrawalReason)
                .HasMaxLength(1000);

            builder.Property(a => a.ReferralSource)
                .HasMaxLength(100);

            builder.Property(a => a.CampaignTrackingCode)
                .HasMaxLength(200);

            builder.Property(a => a.IpAddress)
                .HasMaxLength(45); // Supports IPv6

            builder.Property(a => a.UserAgent)
                .HasMaxLength(500);

            // Enums
            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue("Draft");

            // Indexes
            builder.HasIndex(a => a.ApplicationNumber).IsUnique();
            builder.HasIndex(a => a.ApplicantProfileId);
            builder.HasIndex(a => a.CompanyId);
            builder.HasIndex(a => a.LocationId);
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => a.SubmissionDate);
            builder.HasIndex(a => new { a.CompanyId, a.SubmissionDate });
            builder.HasIndex(a => new { a.ApplicantProfileId, a.CompanyId }).IsUnique()
                .HasFilter("[Status] NOT IN ('Rejected', 'Withdrawn', 'Archived')");

            // Relationships
            builder.HasMany(a => a.ApplicationAnswers)
                .WithOne(aa => aa.Application)
                .HasForeignKey(aa => aa.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.StatusHistory)
                .WithOne(ash => ash.Application)
                .HasForeignKey(ash => ash.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Comments)
                .WithOne(ac => ac.Application)
                .HasForeignKey(ac => ac.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Assessments)
                .WithOne(aa => aa.Application)
                .HasForeignKey(aa => aa.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Documents)
                .WithOne(ad => ad.Application)
                .HasForeignKey(ad => ad.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Computed column for application age (SQL Server specific)
            if (builder.Metadata.GetDatabase().IsSqlServer())
            {
                builder.Property(a => a.CreatedAt)
                    .HasComputedColumnSql("GETUTCDATE()", stored: false);
            }
        }
    }

    public class ApplicationAnswerConfiguration : IEntityTypeConfiguration<ApplicationAnswer>
    {
        public void Configure(EntityTypeBuilder<ApplicationAnswer> builder)
        {
            builder.ToTable("ApplicationAnswers");

            builder.HasKey(aa => aa.Id);

            builder.Property(aa => aa.AnswerText)
                .HasMaxLength(4000);

            builder.Property(aa => aa.AnswerJson)
                .HasMaxLength(8000); // For JSON data

            builder.Property(aa => aa.FilePath)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(aa => new { aa.ApplicationId, aa.CompanyFieldId })
                .IsUnique();

            // Relationships
            builder.HasOne(aa => aa.CompanyField)
                .WithMany(cf => cf.ApplicationAnswers)
                .HasForeignKey(aa => aa.CompanyFieldId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ApplicationStatusHistoryConfiguration : IEntityTypeConfiguration<ApplicationStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatusHistory> builder)
        {
            builder.ToTable("ApplicationStatusHistories");

            builder.HasKey(ash => ash.Id);

            builder.Property(ash => ash.ChangedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ash => ash.Reason)
                .HasMaxLength(500);

            builder.Property(ash => ash.Notes)
                .HasMaxLength(2000);

            // Enums
            builder.Property(ash => ash.PreviousStatus)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(ash => ash.NewStatus)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Indexes
            builder.HasIndex(ash => ash.ApplicationId);
            builder.HasIndex(ash => new { ash.ApplicationId, ash.CreatedAt });
        }
    }
}