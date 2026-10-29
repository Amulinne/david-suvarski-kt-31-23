using david_suvarski_kt_31_23.Contracts;
using david_suvarski_kt_31_23.Database;
using david_suvarski_kt_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace david_suvarski_kt_31_23.Interfaces.CreditsInterfaces;

public interface ICreditService
{
    Task<CreditResult[]> GetStudentCreditsAsync(int studentId, CancellationToken cancellationToken = default);
    Task<int> AddCreditAsync(CreateCreditRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateCreditAsync(int creditId, UpdateCreditRequest request, CancellationToken cancellationToken = default);
}

public class CreditService : ICreditService
{
    private readonly StudentDbContext _dbContext;

    public CreditService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreditResult[]> GetStudentCreditsAsync(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Credits
            .AsNoTracking()
            .Where(credit => credit.StudentId == studentId)
            .Select(credit => new CreditResult
            {
                CreditId = credit.CreditId,
                DisciplineId = credit.DisciplineId,
                IsPassed = credit.IsPassed
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<int> AddCreditAsync(
        CreateCreditRequest request,
        CancellationToken cancellationToken = default)
    {
        var credit = new Credit
        {
            IsPassed = request.IsPassed,
            StudentId = request.StudentId,
            DisciplineId = request.DisciplineId
        };

        _dbContext.Credits.Add(credit);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return credit.CreditId;
    }

    public async Task<bool> UpdateCreditAsync(
        int creditId,
        UpdateCreditRequest request,
        CancellationToken cancellationToken = default)
    {
        var credit = await _dbContext.Credits
            .FirstOrDefaultAsync(item => item.CreditId == creditId, cancellationToken);

        if (credit is null)
        {
            return false;
        }

        credit.IsPassed = request.IsPassed;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
