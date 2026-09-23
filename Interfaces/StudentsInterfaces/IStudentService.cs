using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Filters.StudentFilters.StudentGroupFilter;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;

public interface IStudentService
{
    Task<Student[]> GetStudentsByGroupAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default);
}

public class StudentService : IStudentService
{
    private readonly StudentDbContext _dbContext;

    public StudentService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student[]> GetStudentsByGroupAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default)
    {
        var students = await _dbContext.Set<Student>()
            .Where(student => student.Group.Name == filter.GroupName)
            .ToArrayAsync(cancellationToken);

        return students;
    }
}
