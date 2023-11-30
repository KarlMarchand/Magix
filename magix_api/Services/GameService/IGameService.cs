using magix_api.Dtos;
using magix_api.Dtos.GameDto;

namespace magix_api.Services.GameService
{
    public interface IGameService
    {
        Task<ServiceResponse<string>> JoinGameAsync(string playerKey, string type, string? mode, string? privateKey);
        Task<ServiceResponse<GameStateContainerDto>> ObserveGameAsync(string playerKey, string username);
        Task<ServiceResponse<GameStateContainerDto>> GameActionAsync(string playerKey, GameActionDto gameAction);
        Task<ServiceResponse<GameStateContainerDto>> GetGameStateAsync(string playerKey);
        Task<ServiceResponse<bool>> SaveGameResultAsync(int playerId, string opponent, bool victory, Guid deckId);
        Task<ServiceResponse<PaginatedResponse<GameResultDto>>> GetGamesHistoryAsync(int playerIdInt, int pageNumber, int pageSize);
    }
}