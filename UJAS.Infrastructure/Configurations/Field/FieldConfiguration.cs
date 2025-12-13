using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UJAS.Core.Entities.Field;

namespace UJAS.Infrastructure.Configurations.Field
{
    public class SystemFieldConfiguration : IEntityTypeConfiguration<SystemField>
    {
        public void Configure(EntityTypeBuilder<SystemField> builder)
        {
            builder.ToTable("SystemFields");

            builder.HasKey(sf => sf.Id);

            builder.Property(sf => sf.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sf => sf.Label)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sf => sf.Section)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sf => sf.ValidationRules)
                .HasMaxLength(4000); // JSON

            builder.Property(sf => sf.DefaultValue)
                .HasMaxLength(500);

            builder.Property(sf => sf.HelpText)
                .HasMaxLength(1000);

            builder.Property(sf => sf.PlaceholderText)
                .HasMaxLength(200);

            builder.Property(sf => sf.Options)
                .HasMaxLength(4000); // JSON

            // Enums
            builder.Property(sf => sf.FieldType)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(sf => sf.FieldCategory)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(sf => sf.PrivacyLevel)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Indexes
            builder.HasIndex(sf => sf.Name).IsUnique();
            builder.HasIndex(sf => sf.FieldCategory);
            builder.HasIndex(sf => sf.DisplayOrder);
            builder.HasIndex(sf => sf.IsDefault);
        }
    }

    public class CompanyFieldConfiguration : IEntityTypeConfiguration<CompanyField>
    {
        public void Configure(EntityTypeBuilder<CompanyField> builder)
        {
            builder.ToTable("CompanyFields");

            builder.HasKey(cf => cf.Id);

            builder.Property(cf => cf.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cf => cf.Label)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(cf => cf.Section)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cf => cf.ValidationRules)
                .HasMaxLength(4000); // JSON

            builder.Property(cf => cf.DefaultValue)
                .HasMaxLength(500);

            builder.Property(cf => cf.HelpText)
                .HasMaxLength(1000);

            builder.Property(cf => cf.PlaceholderText)
                .HasMaxLength(200);

            builder.Property(cf => cf.Options)
                .HasMaxLength(4000); // JSON

            builder.Property(cf => cf.ConditionalLogic)
                .HasMaxLength(4000); // JSON

            builder.Property(cf => cf.AllowedFileTypes)
                .HasMaxLength(200);

            // Enums
            builder.Property(cf => cf.FieldType)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(cf => cf.FieldCategory)
                .HasConversion<string>()
                .HasMaxLength(50);

            // Indexes
            builder.HasIndex(cf => new { cf.CompanyId, cf.Name }).IsUnique();
            builder.HasIndex(cf => cf.CompanyId);
            builder.HasIndex(cf => cf.IsCustomField);
            builder.HasIndex(cf => cf.IsEnabled);
            builder.HasIndex(cf => cf.ApplyToAllLocations);

            // Relationships
            builder.HasMany(cf => cf.LocationFields)
                .WithOne(lf => lf.CompanyField)
                .HasForeignKey(lf => lf.CompanyFieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class LocationFieldConfiguration : IEntityTypeConfiguration<LocationField>
    {
        public void Configure(EntityTypeBuilder<LocationField> builder)
        {
            builder.ToTable("LocationFields");

            builder.HasKey(lf => lf.Id);

            builder.Property(lf => lf.CustomLabel)
                .HasMaxLength(200);

            builder.Property(lf => lf.CustomHelpText)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(lf => new { lf.LocationId, lf.CompanyFieldId })
                .IsUnique();
        }
    }
}