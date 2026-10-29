namespace david_suvarski_kt_31_23.Contracts;

public class GradeResult
{
    public int GradeId { get; set; }
    public int DisciplineId { get; set; }
    public int Value { get; set; }
}

public class AverageGradeResult
{
    public double AverageGrade { get; set; }
}

public class CreateGradeRequest
{
    public int Value { get; set; }
    public int StudentId { get; set; }
    public int DisciplineId { get; set; }
}

public class UpdateGradeRequest
{
    public int Value { get; set; }
}
