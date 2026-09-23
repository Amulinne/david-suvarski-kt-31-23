using david_suvarski_kt_31_23.Database.Helpers;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace david_suvarski_kt_31_23.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(TableName);

        builder
            .HasKey(discipline => discipline.DisciplineId)
            .HasName($"pk_{TableName}_discipline_id");

        builder.Property(discipline => discipline.DisciplineId)
            .HasColumnName("c_discipline_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(discipline => discipline.Name)
            .HasColumnName("c_discipline_name")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(discipline => discipline.IsDeleted)
            .HasColumnName("c_discipline_is_deleted")
            .HasColumnType(ColumnType.Bool)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
