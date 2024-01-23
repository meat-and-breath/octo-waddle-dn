using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;

namespace OctoWaddle.UseCases;

public class GetAllContracts
{
    private readonly ContractRepository _contractRepository;

    public GetAllContracts(ContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<List<Contract>> Execute()
    {
        var allContracts = await _contractRepository.GetAllContracts();
        return allContracts;
    }
}
