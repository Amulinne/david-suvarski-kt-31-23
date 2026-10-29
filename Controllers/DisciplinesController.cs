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

    [HttpPost("filter")]
    public async Task<ActionResult<DisciplineResult[]>> GetDisciplinesByFiltersAsync(
        DisciplineFilter filter,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _disciplineService.GetDisciplinesByFiltersAsync(filter, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> AddDisciplineAsync(
        CreateDisciplineRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _disciplineService.AddDisciplineAsync(request, cancellationToken);
        return Ok(new { id });
    }

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
