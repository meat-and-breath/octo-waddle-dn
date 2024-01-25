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
        var contracts = _repo.FindAll(contract => teamGuid.Equals(contract.TeamGuid));
        return Task.FromResult(contracts);
    }

    public Task<Contract?> GetCurrentContractForPlayer(PlayerGuid playerGuid)
    {
        var candidates = GetAllContractsForPlayer(playerGuid).Result;
        // TODO this is not correct business logi
        if (candidates.Count > 1)
        {
            throw new Exception($"Found multiple active contracts for {playerGuid.ToString()}. This is why you should have implemented validation on upsert.");
        }
        return Task.FromResult(candidates.FirstOrDefault());
    
    }
    public async Task<List<Contract>> GetCurrentContractsForTeam(TeamGuid teamGuid)
    {
        var effectiveDate = DateOnly.FromDateTime(DateTime.Now);

        List<Contract> candidates = await GetAllContractsForTeam(teamGuid);
        candidates = candidates.Where(contract => contract.IsActiveOn(effectiveDate)).ToList();

        return candidates.ToList();
    }

    public Task<Contract> UpdateContract(Contract updatedContract)
    {
        // How is this even supposed to work, friend?
        throw new NotImplementedException();
    }

    public Task<List<Contract>> GetAllContracts()
    {
        return Task.FromResult(_repo);
    }

}
