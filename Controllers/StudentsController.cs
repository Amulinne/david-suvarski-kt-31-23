using david_suvarski_kt_31_23.Filters.StudentFilters.StudentGroupFilter;
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

    [HttpPost(Name = "GetStudentsByGroup")]
    public async Task<ActionResult> GetStudentsByGroupAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default)
    {
        var students = await _studentService.GetStudentsByGroupAsync(
            filter,
            cancellationToken);

        return Ok(students);
    }
}
