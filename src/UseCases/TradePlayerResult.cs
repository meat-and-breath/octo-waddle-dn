
using OctoWaddle.Domain.Entities;

namespace OctoWaddle.UseCases;

public struct TradePlayerResult
{
    public TradePlayerResult(Contract newContract, Contract previousContract)
    {
        NewContract = newContract;
        PreviousContract = previousContract;
    }

    public Contract NewContract{ get; init; }
    public Contract PreviousContract{ get; init; }
}