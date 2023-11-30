using magix_api.Dtos;

namespace magix_api.Repositories
{
    public interface IGameRepository
    {
        Task<Game> CreateGame(Game game);
        Task<PaginatedResponse<Game>> GetGamesHistoryAsync(int playerIdInt, int pageNumber, int pageSize);
    }
}