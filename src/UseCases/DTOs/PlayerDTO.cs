namespace OctoWaddle;

public readonly struct PlayerDTO
{
    public Guid PlayerGuid {get; init; }
    public string Name { get; init; }
    public DateOnly Birthdate { get; init; }
    public DateOnly ContractStart { get; init; }
    public DateOnly ContractEnd { get; init; }
    public Decimal? SeasonSalary { get; init; }
}
