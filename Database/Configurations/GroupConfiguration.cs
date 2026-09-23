using david_suvarski_kt_31_23.Database.Helpers;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace david_suvarski_kt_31_23.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    private const string TableName = "cd_group";

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(TableName);

        builder
            .HasKey(group => group.GroupId)
            .HasName($"pk_{TableName}_group_id");

        builder.Property(group => group.GroupId)
            .HasColumnName("c_group_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(group => group.Name)
            .HasColumnName("c_group_name")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(group => group.Course)
            .HasColumnName("c_group_course")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.Property(group => group.SpecialtyId)
            .HasColumnName("f_specialty_id")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.Property(group => group.IsDeleted)
            .HasColumnName("c_group_is_deleted")
            .HasColumnType(ColumnType.Bool)
            .HasDefaultValue(false)
            .IsRequired();

        builder
            .HasOne(group => group.Specialty)
            .WithMany(specialty => specialty.Groups)
            .HasForeignKey(group => group.SpecialtyId)
            .HasConstraintName("fk_f_specialty_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(group => group.SpecialtyId)
            .HasDatabaseName($"idx_{TableName}_f_specialty_id");

        builder.Navigation(group => group.Specialty)
            .AutoInclude();
    }
}
