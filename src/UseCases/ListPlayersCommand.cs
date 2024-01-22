using OctoWaddle.Domain.Entities;

namespace OctoWaddle.UseCases;

public struct ListPlayersCommand
{
    required public Team Team;

    public DateOnly? EffectiveDate;
}
