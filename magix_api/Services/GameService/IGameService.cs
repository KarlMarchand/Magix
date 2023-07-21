using magix_api.Dtos.GameDto;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.GameService
{
    public interface IGameService
    {
        Task<ServiceResponse<string>> JoinGameAsync(string playerKey, string type, string? mode, string? privateKey);
        Task<ServiceResponse<GameStateContainerDto>> ObserveGameAsync(string playerKey, string username);
        Task<ServiceResponse<GameStateContainerDto>> GameActionAsync(string playerKey, GameActionDto gameAction);
        Task<ServiceResponse<GameStateContainerDto>> GetGameStateAsync(string playerKey);
        Task<ServiceResponse<bool>> SaveGameResultAsync(int playerId, string opponent, bool victory, int deckId);
    }
}