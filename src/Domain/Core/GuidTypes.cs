namespace OctoWaddle.Domain;

#pragma warning disable MA0048

public abstract class SpecificGuid
{
    public Guid Value { get; init; }

    protected SpecificGuid()
    {
        Value = Guid.NewGuid();
    }

    protected SpecificGuid(Guid guid)
    {
        Value = guid;
    }
}

public class PlayerGuid : SpecificGuid
{
    public PlayerGuid()
    {
    }

    public PlayerGuid(Guid guid) : base(guid)
    {
    }
}

public class OwnerGuid : SpecificGuid
{
    public OwnerGuid()
    {
    }

    public OwnerGuid(Guid guid) : base(guid)
    {
    }
}

public class TeamGuid : SpecificGuid
{
    public TeamGuid()
    {
    }

    public TeamGuid(Guid guid) : base(guid)
    {
    }
}
