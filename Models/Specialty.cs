namespace david_suvarski_kt_31_23.Models;

public class Specialty
{
    public int SpecialtyId { get; set; }
    public string Title { get; set; } = null!;
    public string Code { get; set; } = null!;

    public ICollection<Group> Groups { get; set; } = new List<Group>();
}
