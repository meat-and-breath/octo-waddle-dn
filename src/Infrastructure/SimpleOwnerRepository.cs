using OctoWaddle.Domain.Entities;

namespace Infrastructure;

public class SimpleOwnerRepository : OwnerRepository
{
    private List<Owner> _repo = new();

    public Task<Owner> AddNewOwner(Owner owner)
    {
        _repo.Add(owner);
        return Task.FromResult(owner);
    }

    public Task<Owner?> GetOwner(OwnerGuid ownerGuid)
    {
        List<Owner> owners = _repo.FindAll(o => ownerGuid.Value.Equals(o.OwnerGuid.Value));
        // TODO make sure there's no more than one
        return Task.FromResult(owners.FirstOrDefault());
    }

    public Task<List<Owner>> GetAllOwners()
    {
        return Task.FromResult(_repo);
    }
}
