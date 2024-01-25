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

    public async Task<List<Owner>> GatAllOwners()
    {
        return await _ownerRepo.GetAllOwners();
    }
}
