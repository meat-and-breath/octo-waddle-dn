namespace OctoWaddle.Domain.Entities;

public class Player
{
    public Player(PlayerGuid PlayerGuid, string name, DateOnly birthdate)
    {
        this.PlayerGuid = PlayerGuid;
        Name = name;
        Birthdate = birthdate;
    }

    public PlayerGuid PlayerGuid {get; init; }

    public string Name { get; set; }

    public DateOnly Birthdate { get; init; }

    // todo: usaful player data here
}