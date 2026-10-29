using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Filters.GroupFilters;
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

    [HttpPost("filter")]
    public async Task<ActionResult<GroupResult[]>> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _groupService.GetGroupsByFiltersAsync(filter, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> AddGroupAsync(
        CreateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _groupService.AddGroupAsync(request, cancellationToken);
        return Ok(new { id });
    }

    [HttpPut("{groupId:int}")]
    public async Task<ActionResult> UpdateGroupAsync(
        int groupId,
        UpdateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _groupService.UpdateGroupAsync(groupId, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{groupId:int}")]
    public async Task<ActionResult> DeleteGroupAsync(
        int groupId,
        CancellationToken cancellationToken = default)
    {
        return await _groupService.DeleteGroupAsync(groupId, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
