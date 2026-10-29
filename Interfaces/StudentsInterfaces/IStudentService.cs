using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Filters.StudentFilters;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;

public interface IStudentService
{
    Task<StudentResult[]> GetStudentsByFiltersAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default);
    Task<int> AddStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateStudentAsync(int studentId, UpdateStudentRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
}

public class StudentService : IStudentService
{
    private readonly StudentDbContext _dbContext;

    public StudentService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentResult[]> GetStudentsByFiltersAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Students.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.GroupName))
        {
            query = query.Where(student => student.Group.Name == filter.GroupName);
        }

        if (!string.IsNullOrWhiteSpace(filter.FullName))
        {
            var fullName = $"%{filter.FullName.Trim()}%";
            query = query.Where(student =>
                EF.Functions.ILike(student.LastName + " " + student.FirstName, fullName) ||
                EF.Functions.ILike(student.FirstName + " " + student.LastName, fullName));
        }

        if (filter.IsDeleted.HasValue)
        {
            query = query.Where(student => student.IsDeleted == filter.IsDeleted.Value);
        }

        return await query
            .Select(student => new StudentResult
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> AddStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
    {
        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            GroupId = request.GroupId,
            IsDeleted = false
        };

        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return student.StudentId;
    }

    public async Task<bool> UpdateStudentAsync(
        int studentId,
        UpdateStudentRequest request,
        CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(item => item.StudentId == studentId, cancellationToken);

        if (student is null)
        {
            return false;
        }

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.GroupId = request.GroupId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(item => item.StudentId == studentId, cancellationToken);

        if (student is null)
        {
            return false;
        }

        student.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
