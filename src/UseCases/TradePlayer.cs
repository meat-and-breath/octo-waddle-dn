using OctoWaddle.Domain.Repositories;
using OctoWaddle.Domain.Entities;

namespace OctoWaddle.UseCases;

public class TradePlayer
{
    private ContractRepository _contractRepository;

    public TradePlayer(ContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<TradePlayerResult> Execute(TradePlayerCommand tradePlayerCommand)
    {
        // Terminate the current contract
        Contract currentContract = await _contractRepository.GetCurrentContractForPlayer(tradePlayerCommand.newContract.PlayerGuid);
        currentContract.EndDate = tradePlayerCommand.newContract.StartDate;
        Contract updatedCurrentContract = await _contractRepository.UpdateContract(currentContract);
        // TODO should we do something with this to validate it worked?

        // Record the new contract
        Contract updatedNewContract = await _contractRepository.AddNewContract(tradePlayerCommand.newContract);

        TradePlayerResult tradePlayerResult = new TradePlayerResult(newContract: updatedNewContract, 
                                                                    previousContract: updatedCurrentContract);

        return tradePlayerResult;
    }
}
