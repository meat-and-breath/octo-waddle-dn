using OctoWaddle.Domain.Entities;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace OctoWaddle.Tests;

public static class TestUtils
{
    private static Random gen = new();

    public static Owner MakeRandomOwner()
    {        
        return new Owner(new OwnerGuid(), RandomName());
    }

    public static Team MakeRandomTeam(Owner owner)
    {        
        var teamName = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords(){Max = 1}).Generate()!;
        var city = RandomizerFactory.GetRandomizer(new FieldOptionsCity()).Generate()!;
        return new Team(new TeamGuid(), 
                        owner.OwnerGuid,
                        teamName,
                        city,
                        RnadomeStartdate());
    }

    public static Player MakeRandomPlayer()
    {
        return new Player(new PlayerGuid(),
                          new Guid().ToString(),
                          RnadomeBirthdate());
    }

    public static Contract MakeRandomContract(TeamGuid teamGuid, PlayerGuid playerGuid)
    {
        return new Contract(playerGuid, teamGuid, RnadomeStartdate(), RnadomeEnddate(), new Decimal(gen.Next(10000, 100000)));
    }

    #region utils for the utils
    public static string RandomName()
    {
        var name = RandomizerFactory.GetRandomizer(new FieldOptionsFullName()).Generate()!;
        return name;
    }

    public static DateOnly RnadomeBirthdate()
    {
        DateTime start = new DateTime(1990, 1, 1);
        DateTime end = new DateTime(2005, 1, 1);
        return RandomDay(start, end);
    }

    public static DateOnly RnadomeStartdate()
    {
        DateTime start = DateTime.Now.AddYears(-5);
        DateTime end = DateTime.Now;
        return RandomDay(start, end);
    }

    public static DateOnly RnadomeEnddate()
    {
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now.AddDays(365*5);
        return RandomDay(start, end);
    }

    public static DateOnly RandomDay(DateTime start, DateTime end)
    {
        // Thanks, StackOverflow https://stackoverflow.com/questions/194863/random-date-in-c-sharp
        int range = (end - start).Days;
        return DateOnly.FromDateTime(start.AddDays(gen.Next(range)));
    }
    #endregion
}
