using OctoWaddle.Domain.Entities;
using OctoWaddle.Domain.Repositories;

namespace Infrastructure;

public class SimplePlayerReporistory : PlayerRepository
{
    private SimpleDB<PlayerGuid, Player> _repo = new ();

    public Task<Player?> GetPlayer(PlayerGuid playerGuid)
    {
        return Task.FromResult(_repo.ReadItem(playerGuid));
    }

    public Task<Player> SaveNewPlayer(Player player)
    {
        Player? existingPlayer = _repo.ReadItem(player.PlayerGuid);
        if (existingPlayer is not null)
        {
            return Task.FromResult(existingPlayer); // TODO this is a really screwing behavioral contract that should probably be fixed
        }
        else 
        {
            _repo.PutItem(player.PlayerGuid, player);
            return Task.FromResult(_repo.ReadItem(player.PlayerGuid)!); // TODO maybe don't swallow a null here, girl
        }
    }

    public Task<Player> UpdatePlayer(Player player)
    {
        return Task.FromResult(_repo.PutItem(player.PlayerGuid, player));
    }
}
