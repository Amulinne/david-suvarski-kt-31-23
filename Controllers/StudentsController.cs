using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Filters.StudentFilters;
using david_suvarski_kt_31_23.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace david_suvarski_kt_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>Получить студентов по фильтрам</summary>
    [HttpPost("filter")]
    public async Task<ActionResult<StudentResult[]>> GetStudentsByFiltersAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _studentService.GetStudentsByFiltersAsync(filter, cancellationToken));
    }

    /// <summary>Добавить студента</summary>
    [HttpPost]
    public async Task<ActionResult> AddStudentAsync(
        CreateStudentRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _studentService.AddStudentAsync(request, cancellationToken);
        return Ok(new { id });
    }

    /// <summary>Изменить студента</summary>
    [HttpPut("{studentId:int}")]
    public async Task<ActionResult> UpdateStudentAsync(
        int studentId,
        UpdateStudentRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _studentService.UpdateStudentAsync(studentId, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }

    /// <summary>Пометить студента удалённым</summary>
    [HttpDelete("{studentId:int}")]
    public async Task<ActionResult> DeleteStudentAsync(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        return await _studentService.DeleteStudentAsync(studentId, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
