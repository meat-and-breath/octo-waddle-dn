using NanoidDotNet;

namespace OctoWaddle.Domain.Entities;

#pragma warning disable MA0048

public abstract class SpecificGuid
{
    public string Value { get; init; }

    protected SpecificGuid()
    {
        Value = Nanoid.Generate(size:8);
    }

    protected SpecificGuid(string id)
    {
        Value = id;
    }
}

public class PlayerGuid : SpecificGuid
{
    public PlayerGuid()
    {
    }

    public PlayerGuid(string id) : base(id)
    {
    }
}

public class OwnerGuid : SpecificGuid
{
    public OwnerGuid()
    {
    }

    public OwnerGuid(string id) : base(id)
    {
    }
}

public class TeamGuid : SpecificGuid
{
    public TeamGuid()
    {
    }

    public TeamGuid(string id) : base(id)
    {
    }
}
