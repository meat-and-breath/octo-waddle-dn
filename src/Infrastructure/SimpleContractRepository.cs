using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;

namespace Infrastructure;

public class SimpleContractRepository : ContractRepository
{
    private List<Contract> _repo = new ();

    public Task<Contract> AddNewContract(Contract newContract)
    {
        // TODO validation & duplicate checking?
        _repo.Add(newContract);
        return Task.FromResult(newContract);
    }

    public Task<List<Contract>> GetAllContractsForPlayer(PlayerGuid playerGuid)
    {
        // JFC the inability to implicitly convert List<Contract> to IList<Contract> just killed me here
        //   Gave up and modified the interface to return List, per this SO https://stackoverflow.com/questions/17170/when-to-use-ilist-and-when-to-use-list
        var contracts = _repo.FindAll(contract => playerGuid.Equals(contract.PlayerGuid));
        return Task.FromResult(contracts);
    }

    public Task<List<Contract>> GetAllContractsForTeam(TeamGuid teamGuid)
    {
        var contracts = _repo.FindAll(contract => teamGuid.Equals(contract.PlayerGuid));
        return Task.FromResult(contracts);
    }

    public Task<Contract?> GetCurrentContractForPlayer(PlayerGuid playerGuid)
    {
        var candidates = GetAllContractsForPlayer(playerGuid).Result;
        if (candidates.Count > 1)
        {
            throw new Exception($"Found multiple active contracts for {playerGuid.ToString()}. This is why you should have implemented validation on upsert.");
        }
        return Task.FromResult(candidates.FirstOrDefault());
    }

    public Task<Contract> UpdateContract(Contract updatedContract)
    {
        // How is this even supposed to work, friend?
        throw new NotImplementedException();
    }

    public void DumpData()
    {
        Console.WriteLine($"Dumping SimpleContractRepository {_repo}");
        foreach(var c in _repo)
        {
            Console.WriteLine(c.ToString());
        }
    }
}
