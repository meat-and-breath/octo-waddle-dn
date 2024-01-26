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

    public async Task<TeamDto?> Execute(TeamGuid teamGuid, OwnerGuid? requestorGuid)
    {
        var team = await _teamRepository.GetTeam(teamGuid);
        if (team is null)
        {
            return null;
        }

        // TODO get owner

        var requestorIsOwner = team.OnwerGuid.Value.Equals(requestorGuid?.Value);
        var contracts = await _contractRepository.GetCurrentContractsForTeam(team.TeamGuid);

        List<PlayerDto> players = new ();
        foreach (var contract in contracts)
        {
            var player = await _playerRepository.GetPlayer(contract.PlayerGuid);
            if (player is not null)
            {
                Decimal? salary = requestorIsOwner ? contract.SeasonSalary : null;

                // TODO use an automapper
                var p = new PlayerDto{
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

        TeamDto t = new TeamDto{
            TeamGuid = team.TeamGuid.Value,
            Name = team.Name,
            Location = team.Location,
            CreationDate = team.CreationDate,
            Owner = new OwnerDto { OwnerGuid = team.OnwerGuid.Value }, // TODO fix this
            Players = players
        };

        return t;
    }
}
