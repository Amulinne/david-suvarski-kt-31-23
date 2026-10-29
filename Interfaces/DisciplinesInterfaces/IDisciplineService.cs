using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Filters.DisciplineFilters;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.DisciplinesInterfaces;

public interface IDisciplineService
{
    Task<DisciplineResult[]> GetDisciplinesByFiltersAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);
    Task<int> AddDisciplineAsync(CreateDisciplineRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateDisciplineAsync(int disciplineId, UpdateDisciplineRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default);
}

public class DisciplineService : IDisciplineService
{
    private readonly StudentDbContext _dbContext;

    public DisciplineService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DisciplineResult[]> GetDisciplinesByFiltersAsync(
        DisciplineFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Disciplines.AsNoTracking().AsQueryable();

        if (filter.IsHumanitarian.HasValue)
        {
            query = query.Where(discipline =>
                discipline.IsHumanitarian == filter.IsHumanitarian.Value);
        }

        if (filter.IsDeleted.HasValue)
        {
            query = query.Where(discipline =>
                discipline.IsDeleted == filter.IsDeleted.Value);
        }

        return await query
            .Select(discipline => new DisciplineResult
            {
                DisciplineId = discipline.DisciplineId,
                Name = discipline.Name
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> AddDisciplineAsync(
        CreateDisciplineRequest request,
        CancellationToken cancellationToken = default)
    {
        var discipline = new Discipline
        {
            Name = request.Name,
            IsHumanitarian = request.IsHumanitarian,
            IsDeleted = false
        };

        _dbContext.Disciplines.Add(discipline);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return discipline.DisciplineId;
    }

    public async Task<bool> UpdateDisciplineAsync(
        int disciplineId,
        UpdateDisciplineRequest request,
        CancellationToken cancellationToken = default)
    {
        var discipline = await _dbContext.Disciplines
            .FirstOrDefaultAsync(item => item.DisciplineId == disciplineId, cancellationToken);

        if (discipline is null)
        {
            return false;
        }

        discipline.Name = request.Name;
        discipline.IsHumanitarian = request.IsHumanitarian;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteDisciplineAsync(
        int disciplineId,
        CancellationToken cancellationToken = default)
    {
        var discipline = await _dbContext.Disciplines
            .FirstOrDefaultAsync(item => item.DisciplineId == disciplineId, cancellationToken);

        if (discipline is null)
        {
            return false;
        }

        discipline.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
