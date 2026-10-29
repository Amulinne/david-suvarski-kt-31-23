namespace david_suvarski_kt_31_23.Contracts;

public class DisciplineResult
{
    public int DisciplineId { get; set; }
    public string Name { get; set; } = null!;
}

public class CreateDisciplineRequest
{
    public string Name { get; set; } = null!;
    public bool IsHumanitarian { get; set; }
}

public class UpdateDisciplineRequest : CreateDisciplineRequest
{
}
