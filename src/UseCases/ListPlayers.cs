using OctoWaddle.Domain;
using OctoWaddle.Domain.Core;
using OctoWaddle.Domain.Repositories;

namespace OctoWaddle.UseCases;

public class ListPlayers
{
    private PlayerRepository _playerRepository;
    private ContractRepository _contractRepository;

    public ListPlayers(PlayerRepository playerRepository, ContractRepository contractRepository)
    {
        _playerRepository = playerRepository;
        _contractRepository = contractRepository;
    }

    public async Task<ListPlayersResult> Execute(ListPlayersCommand listPlayersCommand)
    {
        DateOnly effectiveDate = listPlayersCommand.EffectiveDate ?? DateOnly.FromDateTime(DateTime.Now);
        IList<Contract> allContracts = 
            await _contractRepository.GetAllContractsForTeam(listPlayersCommand.Team.TeamGuid)
            ?? new List<Contract>();

        var activeContracts = allContracts.Where(c => c.IsActiveOn(effectiveDate));
        var allPlayerGuids = activeContracts.Select(c => c.PlayerGuid);

        IEnumerable<Task<Player>> allPlayersAsync = allPlayerGuids.Select(pg => _playerRepository.GetPlayer(pg));
        Player[] allPlayers = await Task.WhenAll(allPlayersAsync.ToArray());

        var result = new ListPlayersResult{
            players = allPlayers.ToList()
        };

        return result;
    }
}
