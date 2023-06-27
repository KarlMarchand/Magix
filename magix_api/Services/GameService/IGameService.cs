using magix_api.Dtos.GameDto;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.GameService
{
    public interface IGameService
    {
        Task<ServiceResponse<string>> joinGame(IdPlayerDto playerInfos, string type, string? mode, string? privateKey);
        Task<ServiceResponse<GameStateFromServer>> observeGame(IdPlayerDto playerInfos, string username);
        Task<ServiceResponse<GameStateFromServer>> gameAction(IdPlayerDto playerInfos, string actionType, int? cardUid, int? targetUid);
        Task<ServerResponse<GameStateFromServer>> getGameState(IdPlayerDto playerInfos);
        Task<ServerResponse<bool>> saveGameResult(IdPlayerDto playerInfos, string opponent, bool victory, List<Card> deck);
    }
}