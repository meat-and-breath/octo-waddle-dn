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

    private readonly GetTeam _getTeam;

    public GetLeagueInfo(OwnerRepository ownerRepositry, 
                         TeamRepository teamRepository, 
                         PlayerRepository playerRepository, 
                         ContractRepository contractRepository,
                         GetTeam getTeam)
    {
        _ownerRepositry = ownerRepositry;
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _contractRepository = contractRepository;
        _getTeam = getTeam;
    }

    public async Task<LeagueDto> Execute(Owner? loggedInOwner)
    {
        // TODO don't ignore the logged-in owner
        var allTeams = await _teamRepository.GetAllTeams();
        
        // TODO I really want to interleave the asyn code here but am leaving it for later
        //      or use continuation passing or something
        List<TeamDto> teams = new ();
        foreach (var team in allTeams)
        {
            var teamDto = await _getTeam.Execute(team.TeamGuid, loggedInOwner?.OwnerGuid);
            if (teamDto is {} td)
            {
                teams.Add(td);
            }
        }

        return new LeagueDto{
            Teams = teams
        };
    }
}
