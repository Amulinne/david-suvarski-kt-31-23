namespace david_suvarski_kt_31_23.Models;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int GroupId { get; set; }
    public bool IsDeleted { get; set; }

    public Group Group { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
