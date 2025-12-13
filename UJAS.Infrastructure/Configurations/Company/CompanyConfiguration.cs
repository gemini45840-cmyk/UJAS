using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UJAS.Core.Entities.Company;

namespace UJAS.Infrastructure.Configurations.Company
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.LegalName)
                .HasMaxLength(200);

            builder.Property(c => c.TaxId)
                .HasMaxLength(50);

            builder.Property(c => c.Website)
                .HasMaxLength(200);

            builder.Property(c => c.Industry)
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(2000);

            builder.Property(c => c.LogoUrl)
                .HasMaxLength(500);

            builder.Property(c => c.PrimaryColor)
                .HasMaxLength(7); // #RRGGBB

            builder.Property(c => c.SecondaryColor)
                .HasMaxLength(7);

            builder.Property(c => c.WidgetEmbedCode)
                .HasMaxLength(4000);

            builder.Property(c => c.TimeZone)
                .HasMaxLength(50)
                .HasDefaultValue("America/New_York");

            // Indexes
            builder.HasIndex(c => c.Name);
            builder.HasIndex(c => c.IsActive);
            builder.HasIndex(c => c.SubscriptionEndDate);

            // Relationships
            builder.HasOne(c => c.Settings)
                .WithOne(cs => cs.Company)
                .HasForeignKey<CompanySettings>(cs => cs.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Locations)
                .WithOne(l => l.Company)
                .HasForeignKey(l => l.CompanyId)
                .IsRequired();
        }
    }

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.AddressLine1)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.AddressLine2)
                .HasMaxLength(200);

            builder.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.StateProvince)
                .HasMaxLength(100);

            builder.Property(l => l.ZipPostalCode)
                .HasMaxLength(20);

            builder.Property(l => l.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Phone)
                .HasMaxLength(20);

            builder.Property(l => l.Email)
                .HasMaxLength(100);

            // Indexes
            builder.HasIndex(l => l.CompanyId);
            builder.HasIndex(l => l.Code).IsUnique();
            builder.HasIndex(l => l.IsActive);
            builder.HasIndex(l => l.IsHeadquarters);

            // Spatial index for coordinates
            if (builder.Metadata.GetDatabase().IsSqlServer())
            {
                builder.HasIndex(l => new { l.Latitude, l.Longitude });
            }
        }
    }

    public class CompanySettingsConfiguration : IEntityTypeConfiguration<CompanySettings>
    {
        public void Configure(EntityTypeBuilder<CompanySettings> builder)
        {
            builder.ToTable("CompanySettings");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.AutoReplyMessage)
                .HasMaxLength(2000);

            builder.Property(cs => cs.DefaultLanguage)
                .HasMaxLength(10)
                .HasDefaultValue("en-US");

            builder.Property(cs => cs.DateFormat)
                .HasMaxLength(20)
                .HasDefaultValue("MM/dd/yyyy");

            builder.Property(cs => cs.ApplicationRetentionDays)
                .HasDefaultValue(365);
        }
    }
}