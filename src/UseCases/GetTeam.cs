using OctoWaddle.Domain;
using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;

namespace OctoWaddle.UseCases;

public class GetTeam
{
    private readonly TeamRepository _teamRepository;
    private readonly ContractRepository _contractRepository;
    private readonly PlayerRepository _playerRepository;

    public GetTeam(TeamRepository teamRepository, ContractRepository contractRepository, PlayerRepository playerRepository)
    {
        _teamRepository = teamRepository;
        _contractRepository = contractRepository;
        _playerRepository = playerRepository;
    }

    public async Task<TeamDTO?> Execute(TeamGuid teamGuid, OwnerGuid? requestorGuid)
    {
        var team = await _teamRepository.GetTeam(teamGuid);
        if (team is null)
        {
            return null;
        }

        var requestorIsOwner = team.OnwerGuid.Equals(requestorGuid);
        var contracts = await _contractRepository.GetCurrentContractsForTeam(team.TeamGuid);

        List<PlayerDTO> players = new ();
        foreach (var contract in contracts)
        {
            var player = await _playerRepository.GetPlayer(contract.PlayerGuid);
            if (player is not null)
            {
                Decimal? salary = requestorIsOwner ? contract.SeasonSalary : null;

                // TODO use an automapper
                var p = new PlayerDTO{
                    PlayerGuid = player.PlayerGuid.Value,
                    Name = player.Name,
                    Birthdate = player.Birthdate,
                    ContractStart = contract.StartDate,
                    ContractEnd = contract.EndDate,
                    SeasonSalary = salary
                };

                players.Add(p);                    
            }
        }

        TeamDTO t = new TeamDTO{
            TeamGuid = team.TeamGuid.Value,
            Name = team.Name,
            Location = team.Location,
            CreationDate = team.CreationDate,
            Owner = new OwnerDTO { OwnerGuid = team.OnwerGuid.Value }, // TODO fix this
            Players = players
        };

        return t;
    }
}
