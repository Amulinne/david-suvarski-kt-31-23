namespace david_suvarski_kt_31_23.Models;

public class Credit
{
    public int CreditId { get; set; }
    public bool IsPassed { get; set; }
    public int StudentId { get; set; }
    public int DisciplineId { get; set; }

    public Student Student { get; set; } = null!;
    public Discipline Discipline { get; set; } = null!;
}
