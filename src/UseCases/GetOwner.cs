using OctoWaddle.Domain;
using OctoWaddle.Domain.Entities;

namespace OctoWaddle.UseCases;

public class GetOwner
{
    private readonly OwnerRepository _ownerRepo;

    public GetOwner(OwnerRepository ownerRepo)
    {
        _ownerRepo = ownerRepo;
    }

    public async Task<OwnerDto?> Execute(OwnerGuid ownerGuid)
    {
        var owner = await _ownerRepo.GetOwner(ownerGuid);
        if (owner is Owner o)
        {
            return new OwnerDto{
                OwnerGuid = o.OwnerGuid.Value,
                Name = o.Name
            };
        }

        return null;
    }
}
