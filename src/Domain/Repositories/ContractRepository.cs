using OctoWaddle.Domain.Entities;

namespace OctoWaddle.Domain.Repositories;

public interface ContractRepository
{
    public Task<Contract?> GetCurrentContractForPlayer(PlayerGuid playerGuid);

    public Task<List<Contract>> GetAllContractsForPlayer(PlayerGuid playerGuid);

    public Task<List<Contract>> GetAllContractsForTeam(TeamGuid teamGuid);

    public Task<Contract> UpdateContract(Contract updatedContract);

    public Task<Contract> AddNewContract(Contract newContract);
}