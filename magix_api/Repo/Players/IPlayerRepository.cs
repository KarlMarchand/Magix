using magix_api.Dtos.PlayerDto;

namespace magix_api.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(Player playerInfos);
        Task<Player> GetProfile(int playerId);
    }
}