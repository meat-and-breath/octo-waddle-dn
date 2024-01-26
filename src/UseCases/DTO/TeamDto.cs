namespace OctoWaddle;

public readonly struct TeamDto
{
    public string TeamGuid { get; init; }

    public string Name { get; init; }

    public string Location { get; init; }

    public DateOnly CreationDate { get; init; }

    // reference fields
    public OwnerDto Owner { get ; init; }

    public List<PlayerDto> Players {get; init; }
}
