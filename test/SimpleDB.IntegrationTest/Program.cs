
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;
using OctoWaddle.Tests;
using OctoWaddle.UseCases;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<PlayerRepository, SimplePlayerReporistory>();
        services.AddSingleton<ContractRepository, SimpleContractRepository>();
        services.AddTransient<ListPlayers>();
    })
    .Build();


// Setup
TeamGuid team1guid = new();
Team team1 = new Team(team1guid, new OwnerGuid(), "a name", "a loclation", DateOnly.FromDateTime(DateTime.Now));

PlayerRepository playerRepository = host.Services.GetService<PlayerRepository>()!;
ContractRepository contractRepository = host.Services.GetService<ContractRepository>()!;

for (var i = 0; i < 12; i++)
{
    var player = TestUtils.MakeRandomPlayer();
    var contract = TestUtils.MakeRandomContract(team1guid, player.PlayerGuid);

    var addPlayer = playerRepository.AddNewPlayer(player);
    var addContract = contractRepository.AddNewContract(contract);
    Task.WaitAll(addPlayer, addContract);
}

// Debug
Console.WriteLine("Dumping All Contracts");
foreach (var contract in await contractRepository.GetAllContracts())
{
    Console.WriteLine(contract);
}

// Test
ListPlayers listPlayers = host.Services.GetService<ListPlayers>()!;

Console.WriteLine($"Got here, friends: {listPlayers}");

var lpc = new ListPlayersCommand { Team = team1 };
var lpr = await listPlayers.Execute(lpc);
var team1Players = lpr.players;

Console.WriteLine($"The {team1.Location} {team1.Name} roster:");
foreach (var player in team1Players)
{
    Console.WriteLine($"\t{player.Name} born {player.Birthdate}");
}
Console.WriteLine();
Console.WriteLine("Don't you with the data model also gave you their salaires right now?");
