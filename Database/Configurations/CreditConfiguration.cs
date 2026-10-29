using david_suvarski_kt_31_23.Database.Helpers;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace david_suvarski_kt_31_23.Database.Configurations;

public class CreditConfiguration : IEntityTypeConfiguration<Credit>
{
    private const string TableName = "cd_credit";

    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(credit => credit.CreditId)
            .HasName($"pk_{TableName}_credit_id");

        builder.Property(credit => credit.CreditId)
            .HasColumnName("c_credit_id")
            .HasColumnType(ColumnType.Int)
            .ValueGeneratedOnAdd();

        builder.Property(credit => credit.IsPassed)
            .HasColumnName("c_credit_is_passed")
            .HasColumnType(ColumnType.Bool)
            .IsRequired();

        builder.Property(credit => credit.StudentId)
            .HasColumnName("f_student_id")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.Property(credit => credit.DisciplineId)
            .HasColumnName("f_discipline_id")
            .HasColumnType(ColumnType.Int)
            .IsRequired();

        builder.HasOne(credit => credit.Student)
            .WithMany(student => student.Credits)
            .HasForeignKey(credit => credit.StudentId)
            .HasConstraintName("fk_credit_student_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(credit => credit.Discipline)
            .WithMany(discipline => discipline.Credits)
            .HasForeignKey(credit => credit.DisciplineId)
            .HasConstraintName("fk_credit_discipline_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(credit => credit.StudentId)
            .HasDatabaseName($"idx_{TableName}_f_student_id");

        builder.HasIndex(credit => credit.DisciplineId)
            .HasDatabaseName($"idx_{TableName}_f_discipline_id");
    }
}
