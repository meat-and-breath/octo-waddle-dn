using OctoWaddle.Domain.Entities;

namespace OctoWaddle.Domain;

public interface OwnerRepository
{
    public Task<List<Owner>> GetAllOwners();

    public Task<Owner> AddNewOwner(Owner owner);

    public Task<Owner?> GetOwner(OwnerGuid ownerGuid);
}
