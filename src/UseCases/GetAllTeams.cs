using OctoWaddle.Domain;
using OctoWaddle.Domain.Entities;

namespace OctoWaddle.UseCases;

public class GetAllTeams
{
    private readonly TeamRepository _teamRepository;

    public GetAllTeams(TeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<List<Team>> Execute()
    {
        var allTeams = await _teamRepository.GetAllTeams();
        return allTeams;
    }
}
