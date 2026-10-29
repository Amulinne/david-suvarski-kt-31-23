using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Interfaces.GradesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace david_suvarski_kt_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    /// <summary>Получить средний балл по дисциплине в группе</summary>
    [HttpGet("average/discipline/{disciplineId:int}/group/{groupId:int}")]
    public async Task<ActionResult<AverageGradeResult>> GetAverageByDisciplineAndGroupAsync(
        int disciplineId,
        int groupId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _gradeService.GetAverageByDisciplineAndGroupAsync(
            disciplineId, groupId, cancellationToken));
    }

    /// <summary>Получить оценки студента</summary>
    [HttpGet("student/{studentId:int}")]
    public async Task<ActionResult<GradeResult[]>> GetStudentGradesAsync(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _gradeService.GetStudentGradesAsync(studentId, cancellationToken));
    }

    /// <summary>Получить средний балл по курсу</summary>
    [HttpGet("average/course/{course:int}")]
    public async Task<ActionResult<AverageGradeResult>> GetAverageByCourseAsync(
        int course,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _gradeService.GetAverageByCourseAsync(course, cancellationToken));
    }

    /// <summary>Добавить оценку</summary>
    [HttpPost]
    public async Task<ActionResult> AddGradeAsync(
        CreateGradeRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _gradeService.AddGradeAsync(request, cancellationToken);
        return Ok(new { id });
    }

    /// <summary>Изменить оценку</summary>
    [HttpPut("{gradeId:int}")]
    public async Task<ActionResult> UpdateGradeAsync(
        int gradeId,
        UpdateGradeRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _gradeService.UpdateGradeAsync(gradeId, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
