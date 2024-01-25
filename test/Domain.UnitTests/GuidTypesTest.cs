namespace OctoWaddle.Tests;

using OctoWaddle.Domain.Entities;

public class GuidTypesTest
{
    [Fact]
    public void CanCreatePlayerGuidFromGuid()
    {
        var nuid = NanoidDotNet.Nanoid.Generate();

        PlayerGuid pg = new PlayerGuid(nuid);

        Assert.Equal(nuid, pg.Value);
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