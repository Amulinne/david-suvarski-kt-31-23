using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Filters.GroupFilters;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.GroupsInterfaces;

public interface IGroupService
{
    Task<GroupResult[]> GetGroupsByFiltersAsync(GroupByFiltersFilter filter, CancellationToken cancellationToken = default);
    Task<int> AddGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateGroupAsync(int groupId, UpdateGroupRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default);
}

public class GroupService : IGroupService
{
    private readonly StudentDbContext _dbContext;

    public GroupService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GroupResult[]> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Groups
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .AsQueryable();

        if (filter.SpecialtyId.HasValue)
        {
            query = query.Where(group => group.SpecialtyId == filter.SpecialtyId.Value);
        }

        if (filter.Course.HasValue)
        {
            query = query.Where(group => group.Course == filter.Course.Value);
        }

        if (filter.IsDeleted.HasValue)
        {
            query = query.Where(group => group.IsDeleted == filter.IsDeleted.Value);
        }

        return await query
            .Select(group => new GroupResult
            {
                GroupId = group.GroupId,
                Name = group.Name
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> AddGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default)
    {
        var group = new Group
        {
            Name = request.Name,
            Course = request.Course,
            SpecialtyId = request.SpecialtyId,
            IsDeleted = false
        };

        _dbContext.Groups.Add(group);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return group.GroupId;
    }

    public async Task<bool> UpdateGroupAsync(
        int groupId,
        UpdateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Groups
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(item => item.GroupId == groupId, cancellationToken);

        if (group is null)
        {
            return false;
        }

        group.Name = request.Name;
        group.Course = request.Course;
        group.SpecialtyId = request.SpecialtyId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Groups
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(item => item.GroupId == groupId, cancellationToken);

        if (group is null)
        {
            return false;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        group.IsDeleted = true;

        await _dbContext.Students
            .Where(student => student.GroupId == groupId)
            .ExecuteUpdateAsync(
                setters => setters.SetProperty(student => student.IsDeleted, true),
                cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}
