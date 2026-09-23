using System.Runtime.Intrinsics.X86;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Filters.GroupFilters.GroupByFiltersFilter;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.GroupsInterfaces;

public interface IGroupService
{
    Task<Group[]> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default);
}

public class GroupService : IGroupService
{
    private readonly StudentDbContext _dbContext;

    public GroupService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Group[]> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<Group>()
            .AsQueryable();

        if (filter.SpecialtyId.HasValue)
        {
            query = query.Where(group =>
                group.SpecialtyId == filter.SpecialtyId.Value);
        }

        if (filter.Course.HasValue)
        {
            query = query.Where(group =>
                group.Course == filter.Course.Value);
        }

        if (filter.IsDeleted.HasValue)
        {
            query = query.Where(group =>
                group.IsDeleted == filter.IsDeleted.Value);
        }

        var groups = await query.ToArrayAsync(cancellationToken);

        return groups;
    }
}
