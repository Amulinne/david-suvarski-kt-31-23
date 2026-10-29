namespace david_suvarski_kt_31_23.Contracts;

public class CreditResult
{
    public int CreditId { get; set; }
    public int DisciplineId { get; set; }
    public bool IsPassed { get; set; }
}

public class CreateCreditRequest
{
    public bool IsPassed { get; set; }
    public int StudentId { get; set; }
    public int DisciplineId { get; set; }
}

public class UpdateCreditRequest
{
    public bool IsPassed { get; set; }
}
