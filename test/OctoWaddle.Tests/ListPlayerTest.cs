using Moq;
using FluentAssertions;
using OctoWaddle.Domain;
using OctoWaddle.Domain.Repositories;
using OctoWaddle.Domain.Core;
using OctoWaddle.UseCases;

namespace OctoWaddle.Tests;

public class ListPlayerTest
{
    [Fact]
    public async Task CanListPlayers()
    {
        var team1 = new Team(new TeamGuid(), new OwnerGuid(),
                            "Wallbangers", "Washington", DateOnly.Parse("1/1/2020", 
                            System.Globalization.CultureInfo.InvariantCulture));
        var team2 = new Team(new TeamGuid(), new OwnerGuid(),
                            "Orcas", "Oregon", DateOnly.Parse("1/1/2019", 
                            System.Globalization.CultureInfo.InvariantCulture));

        var players1 = GeneratePlayers(10);
        var players2 = GeneratePlayers(10);

        var playerRepository = new Mock<PlayerRepository>();
        foreach (Player player in players1.Concat(players2))
        {
            playerRepository.Setup(pr => pr.GetPlayer(player.PlayerGuid))
                            .Returns(Task.FromResult(player));
        }

        IList<Contract> team1Contracts = players1.Select(player => {
            return new Contract(player.PlayerGuid, team1.TeamGuid, 
                                TestUtils.RnadomeStartdate(),
                                TestUtils.RnadomeEnddate(),
                                1000.00M);
        }).ToList();
        IList<Contract> team2Contracts = players2.Select(player => {
            return new Contract(player.PlayerGuid, team2.TeamGuid, 
                                TestUtils.RnadomeStartdate(),
                                TestUtils.RnadomeEnddate(),
                                1000.00M);
        }).ToList();

        var contractRepository = new Mock<ContractRepository>();
        contractRepository.Setup(cr => cr.GetAllContractsForTeam(team1.TeamGuid))
                          .Returns(Task.FromResult(team1Contracts));
        contractRepository.Setup(cr => cr.GetAllContractsForTeam(team2.TeamGuid))
                          .Returns(Task.FromResult(team2Contracts));

        ListPlayers listPlayersUC = new(playerRepository.Object, contractRepository.Object);

        var result1 = await listPlayersUC.Execute(new ListPlayersCommand{ Team = team1 });
        var result2 = await listPlayersUC.Execute(new ListPlayersCommand{ Team = team2 });

        result1.players.Should().Equal(players1);
        result2.players.Should().Equal(players2);

        var tooFarFutureResult1 = await listPlayersUC.Execute(
                                    new ListPlayersCommand{ Team = team1, 
                                                            EffectiveDate = DateOnly.MaxValue});
        var tooFarPastResult2 = await listPlayersUC.Execute(
                                    new ListPlayersCommand{ Team = team2,
                                                            EffectiveDate = DateOnly.MinValue});
        tooFarFutureResult1.players.Should().BeEmpty();
        tooFarPastResult2.players.Should().BeEmpty();

        return;
    }

    public IList<Player> GeneratePlayers(int count)
    {
        List<Player> players = new();
        for (int i = 0; i < count; i++)
        {
            var player = TestUtils.MakeRandomPlayer();
            players.Add(player);
        }

        return players;
    }
}
