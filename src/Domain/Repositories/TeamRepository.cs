using OctoWaddle.Domain.Entities;

namespace OctoWaddle.Domain;

public interface TeamRepository
{
    public Task<List<Team>> GetAllTeams();

    public Task<Team> AddNewTeam(Team team);
}
