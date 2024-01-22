using OctoWaddle.Domain.Entities;

namespace OctoWaddle.Tests;

public static class TestUtils
{
    private static Random gen = new();

    public static Player MakeRandomPlayer()
    {
        return new Player(new PlayerGuid(),
                          new Guid().ToString(),
                          RnadomeBirthdate());
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
}
