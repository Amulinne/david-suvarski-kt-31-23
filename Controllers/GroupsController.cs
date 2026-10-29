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

    /// <summary>Получить группы по фильтрам</summary>
    [HttpPost("filter")]
    public async Task<ActionResult<GroupResult[]>> GetGroupsByFiltersAsync(
        GroupByFiltersFilter filter,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _groupService.GetGroupsByFiltersAsync(filter, cancellationToken));
    }

    /// <summary>Добавить группу</summary>
    [HttpPost]
    public async Task<ActionResult> AddGroupAsync(
        CreateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _groupService.AddGroupAsync(request, cancellationToken);
        return Ok(new { id });
    }

    /// <summary>Изменить группу</summary>
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

    /// <summary>Пометить группу и её студентов удалёнными</summary>
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
