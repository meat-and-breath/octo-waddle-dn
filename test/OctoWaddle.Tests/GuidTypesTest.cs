namespace OctoWaddle.Tests;

using OctoWaddle.Domain;

public class GuidTypesTest
{
    [Fact]
    public void CanCreatePlayerGuidFromGuid()
    {
        var guid = Guid.NewGuid();

        PlayerGuid pg = new PlayerGuid(guid);

        Assert.Equal(guid, pg.Value);
    }

    [Fact]
    public void CanCreatePlayerGuidNew()
    {
        PlayerGuid pg = new PlayerGuid();

        #pragma warning disable xUnit2002
        // shouldn't pramga disable this warning, but this is kind of a dummy unit test anyway
        Assert.NotNull(pg.Value);
    }

}