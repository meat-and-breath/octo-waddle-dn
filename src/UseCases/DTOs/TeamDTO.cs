namespace OctoWaddle;

public readonly struct TeamDTO
{
    public Guid TeamGuid { get; init; }

    public string Name { get; init; }

    public string Location { get; init; }

    public DateOnly CreationDate { get; init; }

    // reference fields
    public OwnerDTO Owner { get ; init; }

    public List<PlayerDTO> Players {get; init; }

}
