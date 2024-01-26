namespace OctoWaddle;

public readonly struct PlayerDTO
{
    public string PlayerGuid {get; init; }
    public string Name { get; init; }
    public DateOnly Birthdate { get; init; }
    public DateOnly ContractStart { get; init; }
    public DateOnly ContractEnd { get; init; }
    public Decimal? SeasonSalary { get; init; }
}
