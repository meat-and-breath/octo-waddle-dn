using OctoWaddle.Domain;
using OctoWaddle.Domain.Repositories;
using OctoWaddle.Tests;

namespace OctoWaddle.UseCases;

public class GenerateRandomLeague
{

    private readonly PlayerRepository _playerRepo;
    private readonly ContractRepository _contractReop;
    private readonly TeamRepository _teamRepository;
    private readonly OwnerRepository _ownerRepository;

    public GenerateRandomLeague(PlayerRepository playerRepo, 
                                ContractRepository contractReop,
                                TeamRepository teamRepository,
                                OwnerRepository ownerRepository)
    {
        _playerRepo = playerRepo;
        _contractReop = contractReop;
        _teamRepository = teamRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task Execute()
    {
        for(var t = 0; t < 4; t++)
        {
            var owner = TestUtils.MakeRandomOwner();
            await _ownerRepository.AddNewOwner(owner);

            var team = TestUtils.MakeRandomTeam(owner);
            await _teamRepository.AddNewTeam(team);

            for (var p = 0; p < 5; p++)
            {
                var player = TestUtils.MakeRandomPlayer();
                var contract = 
                    TestUtils.MakeRandomContract(team.TeamGuid,
                                                 player.PlayerGuid);

                await _playerRepo.AddNewPlayer(player);
                await _contractReop.AddNewContract(contract);
            }
        }
    }
}
