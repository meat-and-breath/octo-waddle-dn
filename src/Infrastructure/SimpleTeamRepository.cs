using OctoWaddle.Domain.Entities;

namespace Infrastructure;

public class SimpleTeamRepository : TeamRepository
{
    private List<Team> _repo = new();

    public Task<Team> AddNewTeam(Team team)
    {
        _repo.Add(team);
        return Task.FromResult(team);
    }

    public Task<List<Team>> GetAllTeams()
    {
        return Task.FromResult(_repo);
    }

    public Task<Team?> GetTeam(TeamGuid teamGuid)
    {
        var team = _repo.Find(t => t.TeamGuid.Equals(teamGuid));
        return Task.FromResult(team);
    }
}
