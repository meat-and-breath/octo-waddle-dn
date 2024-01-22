namespace OctoWaddle.Domain;

public class Owner
{
    public Owner(OwnerGuid guid, string name)
    {
        this.guid = guid;
        Name = name;
    }

    public required OwnerGuid guid {get; init; }

    public string Name { get; set; }
}