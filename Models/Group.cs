namespace david_suvarski_kt_31_23.Models;

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; } = null!;
    public int Course { get; set; }
    public int SpecialtyId { get; set; }
    public bool IsDeleted { get; set; }

    public Specialty Specialty { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
