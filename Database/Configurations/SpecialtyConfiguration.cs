using david_suvarski_kt_31_23.Database.Helpers;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace david_suvarski_kt_31_23.Database.Configurations;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    private const string TableName = "cd_specialty";

    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.ToTable(TableName);

        builder
            .HasKey(specialty => specialty.SpecialtyId)
            .HasName($"pk_{TableName}_specialty_id");

        builder.Property(specialty => specialty.SpecialtyId)
            .HasColumnName("c_specialty_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(specialty => specialty.Title)
            .HasColumnName("c_specialty_title")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(specialty => specialty.Code)
            .HasColumnName("c_specialty_code")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(50)
            .IsRequired();
    }
}
