using magix_api.Dtos.GameDto;
using magix_api.Repositories;
using magix_api.utils;

namespace magix_api.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepo;
        private readonly string _baseApiUrl = "games/";
        private readonly HashSet<string> _validAnswers = new() { "WAITING", "LAST_GAME_WON", "LAST_GAME_LOST", "NOT_IN_GAME" };

        public GameService(IGameRepository gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public async Task<ServiceResponse<GameStateContainerDto>> GameActionAsync(string playerKey, GameActionDto gameAction)
        {
            ServiceResponse<GameStateContainerDto> response = new();

            Dictionary<string, string> data = new() { { "key", playerKey }, { "type", gameAction.actionType } };

            if (gameAction.cardUid != null)
            {
                data.Add("uid", gameAction.targetUid.GetValueOrDefault().ToString());
            }

            if (gameAction.targetUid != null)
            {
                data.Add("targetuid", gameAction.targetUid.GetValueOrDefault().ToString());
            }

            return await ProcessGameResponse(GameServerAPI.CallApi<GameStateFromServerDto>(this.GetUrl("action"), data));
        }

        public async Task<ServiceResponse<GameStateContainerDto>> GetGameStateAsync(string playerKey)
        {
            ServiceResponse<GameStateContainerDto> response = new();

            Dictionary<string, string> data = new() { { "key", playerKey } };

            return await ProcessGameResponse(GameServerAPI.CallApi<GameStateFromServerDto>(this.GetUrl("state"), data));
        }

        public async Task<ServiceResponse<string>> JoinGameAsync(string playerKey, string type, string? mode, string? privateKey)
        {
            ServiceResponse<string> response = new();

            Dictionary<string, string> data = new()
            {
                {"key", playerKey},
                {"type", type},
            };

            if (mode != null)
            {
                data.Add("mode", mode);
            }

            if (privateKey != null)
            {
                data.Add("privateKey", privateKey);
            }

            ServerResponse<string> res = await GameServerAPI.CallApi<string>(this.GetUrl("auto-match"), data);

            if (res.IsValid)
            {
                response.Data = res.Content;
            }
            else
            {
                if (res.Error != null)
                {
                    response.Message = res.Error;
                }
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceResponse<GameStateContainerDto>> ObserveGameAsync(string playerKey, string username)
        {
            ServiceResponse<GameStateContainerDto> response = new();

            Dictionary<string, string> data = new()
            {
                {"key", playerKey},
                {"username", username},
            };

            return await ProcessGameResponse(GameServerAPI.CallApi<GameStateFromServerDto>(this.GetUrl("observe"), data));
        }

        public async Task<ServiceResponse<bool>> SaveGameResultAsync(int playerId, string opponent, bool victory, Guid deckId)
        {
            ServiceResponse<bool> response = new();

            var game = new Game
            {
                PlayerId = playerId,
                Opponent = opponent,
                Won = victory,
                DeckId = deckId,
                Date = DateTime.Now
            };

            var savedGame = await _gameRepo.CreateGame(game);

            if (savedGame != null)
            {
                response.Data = true;
            }
            else
            {
                response.Data = false;
                response.Message = "Failed to create game";
            }

            return response;
        }

        private string GetUrl(string service)
        {
            return this._baseApiUrl + service;
        }

        private async Task<ServiceResponse<GameStateContainerDto>> ProcessGameResponse(
            Task<ServerResponse<GameStateFromServerDto>> gameResponseTask)
        {
            ServiceResponse<GameStateContainerDto> response = new();

            var res = await gameResponseTask;

            if (res.IsValid)
            {
                response.Data = new GameStateContainerDto() { GameState = res.Content };
            }
            else if (res.IsError && _validAnswers.Contains(res.Error!))
            {
                response.Data = new GameStateContainerDto() { Message = res.Error };
            }
            else if (res.IsError)
            {
                response.Success = false;
                response.Message = res.Error!;
            }

            return response;
        }
    }
}