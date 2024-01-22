using System.Numerics;

namespace OctoWaddle.Domain.Entities;

public class Contract
{
    public Contract(PlayerGuid playerGuid, TeamGuid teamGuid, DateOnly startDate, DateOnly endDate, decimal seasonSalary)
    {
        PlayerGuid = playerGuid;
        TeamGuid = teamGuid;
        StartDate = startDate;
        EndDate = endDate;
        SeasonSalary = seasonSalary;
    }

    public PlayerGuid PlayerGuid { get; init; }
    public TeamGuid TeamGuid { get; init; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    // TODO: could use a more complicated salary calc, perf bonuses, etc.
    public Decimal SeasonSalary { get; set; }

    public Boolean IsActiveOn(DateOnly date)
    {
        return StartDate <= date && EndDate >= date;
    }
}
