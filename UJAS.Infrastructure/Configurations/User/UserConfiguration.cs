using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UJAS.Core.Entities.Profile;
using UJAS.Core.Entities.User;

namespace UJAS.Infrastructure.Configurations.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User.tUser>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.User.tUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.NormalizedEmail)
                .HasMaxLength(256);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            builder.HasIndex(u => u.NormalizedEmail)
                .HasDatabaseName("EmailIndex");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Relationships
            builder.HasOne(u => u.ApplicantProfile)
                .WithOne(ap => ap.User)
                .HasForeignKey<ApplicantProfile>(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.CompanyUser)
                .WithOne(cu => cu.User)
                .HasForeignKey<CompanyUser>(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasMany(u => u.UserClaims)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            builder.HasMany(u => u.UserLogins)
                .WithOne(ul => ul.User)
                .HasForeignKey(ul => ul.UserId);

            builder.HasMany(u => u.UserTokens)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId);
        }
    }
}

public class RoleConfiguration : IEntityTypeConfiguration<UJAS.Core.Entities.User.Role>
{
    public void Configure(EntityTypeBuilder<UJAS.Core.Entities.User.Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(256);

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .HasDatabaseName("RoleNameIndex");

        // Relationships
        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .IsRequired();
    }
}

public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        builder.ToTable("CompanyUsers");

        builder.HasKey(cu => cu.Id);

        builder.Property(cu => cu.EmployeeId)
            .HasMaxLength(50);

        builder.Property(cu => cu.Department)
            .HasMaxLength(100);

        builder.Property(cu => cu.Title)
            .HasMaxLength(100);

        // Relationships
        builder.HasOne(cu => cu.Company)
            .WithMany(c => c.CompanyUsers)
            .HasForeignKey(cu => cu.CompanyId)
            .IsRequired();

        builder.HasOne(cu => cu.Location)
            .WithMany(l => l.Managers)
            .HasForeignKey(cu => cu.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(cu => cu.RegionalManagerLocations)
            .WithOne(rml => rml.CompanyUser)
            .HasForeignKey(rml => rml.CompanyUserId)
            .IsRequired();
    }
}

public class RegionalManagerLocationConfiguration : IEntityTypeConfiguration<RegionalManagerLocation>
{
    public void Configure(EntityTypeBuilder<RegionalManagerLocation> builder)
    {
        builder.ToTable("RegionalManagerLocations");

        builder.HasKey(rml => rml.Id);

        // Composite unique constraint
        builder.HasIndex(rml => new { rml.CompanyUserId, rml.LocationId })
            .IsUnique();

        // Relationships
        builder.HasOne(rml => rml.Location)
            .WithMany(l => l.RegionalManagers)
            .HasForeignKey(rml => rml.LocationId)
            .IsRequired();
    }
}