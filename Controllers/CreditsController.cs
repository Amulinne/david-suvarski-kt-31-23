using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Interfaces.CreditsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace david_suvarski_kt_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditsController : ControllerBase
{
    private readonly ICreditService _creditService;

    public CreditsController(ICreditService creditService)
    {
        _creditService = creditService;
    }

    [HttpGet("student/{studentId:int}")]
    public async Task<ActionResult<CreditResult[]>> GetStudentCreditsAsync(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _creditService.GetStudentCreditsAsync(studentId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> AddCreditAsync(
        CreateCreditRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = await _creditService.AddCreditAsync(request, cancellationToken);
        return Ok(new { id });
    }

    [HttpPut("{creditId:int}")]
    public async Task<ActionResult> UpdateCreditAsync(
        int creditId,
        UpdateCreditRequest request,
        CancellationToken cancellationToken = default)
    {
        return await _creditService.UpdateCreditAsync(creditId, request, cancellationToken)
            ? NoContent()
            : NotFound();
    }
}
