using david_suvarski_kt_31_23.Database.Configurations;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Database;

public class StudentDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Credit> Credits { get; set; }

    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
        modelBuilder.ApplyConfiguration(new GradeConfiguration());
        modelBuilder.ApplyConfiguration(new CreditConfiguration());
    }
}
