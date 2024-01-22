using Moq;
using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;
using OctoWaddle.UseCases;
using FluentAssertions;

namespace OctoWaddle.Tests;

public class TradePlayerTest
{
    [Fact]
    public async Task CanTradePlayer()
    {
        var playerGuid = new PlayerGuid();
        var oldTeamGuid = new TeamGuid();
        var newTeamGuid = new TeamGuid();

        var today = DateOnly.FromDateTime(DateTime.Now);

        var startingContract = new Contract(playerGuid, oldTeamGuid,
                                            today.AddMonths(-3),
                                            today.AddMonths(9),
                                            1000.00M);
        var newContract = new Contract(playerGuid, newTeamGuid, 
                                       today, today.AddMonths(9),
                                       1500.00M);

        var contractRepository = new Mock<ContractRepository>();
        contractRepository.Setup(cr => cr.GetCurrentContractForPlayer(playerGuid))
                          .Returns(Task.FromResult(startingContract));
        contractRepository.Setup(cr => cr.UpdateContract(It.IsAny<Contract>()))
                          .Returns((Contract contract) => Task.FromResult(contract));
        contractRepository.Setup(cr => cr.AddNewContract(It.IsAny<Contract>()))
                          .Returns((Contract contract) => Task.FromResult(contract));

        var tradePlayerUC = new TradePlayer(contractRepository.Object);
        var tradePlayerCommand = new TradePlayerCommand{
            newContract = newContract
        };

        TradePlayerResult result = await tradePlayerUC.Execute(tradePlayerCommand);

        result.PreviousContract.EndDate.Should().Be(newContract.StartDate, 
                                "old contract should end on new contract start date");
        Assert.Equal(newContract, result.NewContract);

        return;
    }
}
