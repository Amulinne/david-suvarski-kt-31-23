using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.GradesInterfaces;

public interface IGradeService
{
    Task<AverageGradeResult> GetAverageByDisciplineAndGroupAsync(int disciplineId, int groupId, CancellationToken cancellationToken = default);
    Task<GradeResult[]> GetStudentGradesAsync(int studentId, CancellationToken cancellationToken = default);
    Task<AverageGradeResult> GetAverageByCourseAsync(int course, CancellationToken cancellationToken = default);
    Task<int> AddGradeAsync(CreateGradeRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateGradeAsync(int gradeId, UpdateGradeRequest request, CancellationToken cancellationToken = default);
}

public class GradeService : IGradeService
{
    private readonly StudentDbContext _dbContext;

    public GradeService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AverageGradeResult> GetAverageByDisciplineAndGroupAsync(
        int disciplineId,
        int groupId,
        CancellationToken cancellationToken = default)
    {
        var average = await _dbContext.Grades
            .IgnoreAutoIncludes()
            .Where(grade =>
                grade.DisciplineId == disciplineId &&
                grade.Student.GroupId == groupId)
            .Select(grade => (double?)grade.Value)
            .AverageAsync(cancellationToken);

        return new AverageGradeResult { AverageGrade = average ?? 0 };
    }

    public async Task<GradeResult[]> GetStudentGradesAsync(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Grades
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Where(grade => grade.StudentId == studentId)
            .Select(grade => new GradeResult
            {
                GradeId = grade.GradeId,
                DisciplineId = grade.DisciplineId,
                Value = grade.Value
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<AverageGradeResult> GetAverageByCourseAsync(
        int course,
        CancellationToken cancellationToken = default)
    {
        var average = await _dbContext.Grades
            .IgnoreAutoIncludes()
            .Where(grade => grade.Student.Group.Course == course)
            .Select(grade => (double?)grade.Value)
            .AverageAsync(cancellationToken);

        return new AverageGradeResult { AverageGrade = average ?? 0 };
    }

    public async Task<int> AddGradeAsync(
        CreateGradeRequest request,
        CancellationToken cancellationToken = default)
    {
        var grade = new Grade
        {
            Value = request.Value,
            StudentId = request.StudentId,
            DisciplineId = request.DisciplineId
        };

        _dbContext.Grades.Add(grade);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return grade.GradeId;
    }

    public async Task<bool> UpdateGradeAsync(
        int gradeId,
        UpdateGradeRequest request,
        CancellationToken cancellationToken = default)
    {
        var grade = await _dbContext.Grades
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(item => item.GradeId == gradeId, cancellationToken);

        if (grade is null)
        {
            return false;
        }

        grade.Value = request.Value;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
