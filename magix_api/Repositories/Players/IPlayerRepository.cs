using magix_api.Dtos.PlayerDto;

namespace magix_api.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(Player apiPlayer);
        Task<Player> GetCompleteProfile(int playerId);
        Task<Player> UpdatePlayer(int playerId, Player apiPlayer);
        Task<Player> AddPlayer(Player apiPlayer);
    }
}