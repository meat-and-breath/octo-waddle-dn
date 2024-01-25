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

    public async Task<List<OwnerDTO>> GatAllOwners()
    {
        var owners = await _ownerRepo.GetAllOwners();
        return owners.ToList().Select(o => 
            new OwnerDTO{
                OwnerGuid = o.OwnerGuid.Value,
                Name = o.Name
            }).ToList();
    }
}
