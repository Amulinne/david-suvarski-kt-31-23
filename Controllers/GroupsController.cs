using david_suvarski_kt_31_23.Filters.GroupFilters.GroupByFiltersFilter;
using david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;
using david_suvarski_kt_31_23.Interfaces.GroupsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace david_suvarski_kt_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
    public async Task<ActionResult> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default)
    {
        var groups = await _groupService.GetGroupsByFiltersAsync(
            filter,
            cancellationToken);

        return Ok(groups);
    }
}