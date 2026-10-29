namespace david_suvarski_kt_31_23.Contracts;

public class StudentResult
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class CreateStudentRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int GroupId { get; set; }
}

public class UpdateStudentRequest : CreateStudentRequest
{
}
