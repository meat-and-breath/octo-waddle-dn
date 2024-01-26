using OctoWaddle.Domain.Entities;

namespace OctoWaddle;

public readonly struct OwnerDto
{
    public string OwnerGuid { get; init; }

    public string Name { get; init; }

    public Owner GetOwner()
    {
        return new Owner(new OwnerGuid(OwnerGuid), Name);
    }
}
