namespace OctoWaddle.Domain.Entities;

public class Owner
{
    public Owner(OwnerGuid guid, string name)
    {
        OwnerGuid = guid;
        Name = name;
    }

    public OwnerGuid OwnerGuid {get; init; }

    public string Name { get; set; }
}