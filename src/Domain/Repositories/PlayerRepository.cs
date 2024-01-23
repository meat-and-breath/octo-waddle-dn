using OctoWaddle.Domain.Entities;

namespace OctoWaddle.Domain.Repositories;

public interface PlayerRepository
{
    public Task<Player?> GetPlayer (PlayerGuid playerGuid);

    public Task<Player> UpdatePlayer (Player player);

    public Task<Player> AddNewPlayer (Player player);
}
