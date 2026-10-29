using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Filters.DisciplineFilters;
using david_suvarski_kt_31_23.Interfaces.DisciplinesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace david_suvarski_kt_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class DisciplinesController : ControllerBase
{
    private readonly IDisciplineService _disciplineService;

    public DisciplinesController(IDisciplineService disciplineService)
    {
        _disciplineService = disciplineService;
    }

    /// <summary>Получить дисциплины по фильтрам</summary>
    [HttpPost("filter")]
    public async Task<ActionResult<DisciplineResult[]>> GetDisciplinesByFiltersAsync(
        DisciplineFilter filter,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _disciplineService.GetDisciplinesByFiltersAsync(filter, cancellationToken));
    }

    /// <summary>Добавить дисциплину</summary>
    [HttpPost]
    public async Task<ActionResult> AddDisciplineAsync(
        CreateDisciplineRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _disciplineService.AddDisciplineAsync(request, cancellationToken);
        return Ok(new { id });
    }

    /// <summary>Изменить дисциплину</summary>
    [HttpPut("{disciplineId:int}")]
    public async Task<ActionResult> UpdateDisciplineAsync(
        int disciplineId,
        UpdateDisciplineRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _disciplineService.UpdateDisciplineAsync(disciplineId, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }

    /// <summary>Пометить дисциплину удалённой</summary>
    [HttpDelete("{disciplineId:int}")]
    public async Task<ActionResult> DeleteDisciplineAsync(
        int disciplineId,
        CancellationToken cancellationToken = default)
    {
        return await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
