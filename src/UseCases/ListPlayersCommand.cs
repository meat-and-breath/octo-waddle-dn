using OctoWaddle.Domain;

namespace OctoWaddle.UseCases;

public struct ListPlayersCommand
{
    required public Team Team;

    public DateOnly? EffectiveDate;
}
