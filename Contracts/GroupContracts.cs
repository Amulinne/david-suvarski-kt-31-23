namespace david_suvarski_kt_31_23.Contracts;

public class GroupResult
{
    public int GroupId { get; set; }
    public string Name { get; set; } = null!;
}

public class CreateGroupRequest
{
    public string Name { get; set; } = null!;
    public int Course { get; set; }
    public int SpecialtyId { get; set; }
}

public class UpdateGroupRequest : CreateGroupRequest
{
}
