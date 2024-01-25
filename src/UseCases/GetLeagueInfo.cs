using OctoWaddle.Domain;
using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;

namespace OctoWaddle.UseCases;

public class GetLeagueInfo
{
    private readonly OwnerRepository _ownerRepositry;
    private readonly TeamRepository _teamRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly ContractRepository _contractRepository;

    public GetLeagueInfo(OwnerRepository ownerRepositry, TeamRepository teamRepository, PlayerRepository playerRepository, ContractRepository contractRepository)
    {
        _ownerRepositry = ownerRepositry;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _contractRepository = contractRepository;
    }

    public async Task<LeagueDTO> Execute(Owner? loggedInOwner)
    {
        // TODO don't ignore the logged-in owner
        var allTeams = await _teamRepository.GetAllTeams();
        
        // TODO I really want to interleave the asyn code here but am leaving it for later
        List<TeamDTO> teams = new ();
        foreach (var team in allTeams)
        {
            var contracts = await _contractRepository.GetCurrentContractsForTeam(team.TeamGuid);

            List<PlayerDTO> players = new ();
            foreach (var contract in contracts)
            {
                var player = await _playerRepository.GetPlayer(contract.PlayerGuid);
                if (player is not null)
                {
                    Decimal? salary = null; // TODO salary info should only display for the owner's team 

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
                Owner = new OwnerDTO { OwnerGuid = team.OnwerGuid.Value },
                Players = players
            };
            teams.Add(t);
        }

        return new LeagueDTO{
            Teams = teams
        };
    }
}
