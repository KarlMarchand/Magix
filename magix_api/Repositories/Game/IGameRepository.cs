using magix_api.Dtos;

namespace magix_api.Repositories
{
    public interface IGameRepository
    {
        Task<Game> CreateGameAsync(Game game);
        Task<PaginatedResponse<Game>> GetGamesHistoryAsync(int playerIdInt, int pageNumber, int pageSize);
        Task AddPlayedCardsAsync(int playerId, bool isVictory, List<int> playedCardsIds);
    }
}