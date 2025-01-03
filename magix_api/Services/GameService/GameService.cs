using AutoMapper;
using magix_api.Dtos;
using magix_api.Dtos.CardDto;
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
        private readonly IMapper _mapper;
        private readonly IHeroRepo _heroRepo;

        public GameService(IGameRepository gameRepo, IMapper mapper, IHeroRepo heroRepo)
        {
            _mapper = mapper;
            _gameRepo = gameRepo;
            _heroRepo = heroRepo;
        }

        public async Task<ServiceResponse<GameStateContainerDto>> GameActionAsync(string playerKey, GameActionDto gameAction)
        {
            Dictionary<string, string> data = new() { { "key", playerKey }, { "type", gameAction.actionType } };

            if (gameAction.cardUid != null)
            {
                data.Add("uid", gameAction.targetUid.GetValueOrDefault().ToString());
            }

            if (gameAction.targetUid != null)
            {
                data.Add("targetuid", gameAction.targetUid.GetValueOrDefault().ToString());
            }

            return await ProcessGameResponse(GameServerAPI.CallApi<GameStateFromServerDto>(GetUrl("action"), data));
        }

        public async Task<ServiceResponse<GameStateContainerDto>> GetGameStateAsync(string playerKey)
        {
            Dictionary<string, string> data = new() { { "key", playerKey } };

            //var gameState= GameServerAPI.CallApi<GameStateFromServerDto>(GetUrl("state"), data);

            var gameState = new ServerResponse<GameStateFromServerDto>(new GameStateFromServerDto
            {
                Username = "Karlipouette",
                RemainingTurnTime = 24,
                YourTurn = true,
                HeroPowerAlreadyUsed = false,
                Hp = 30,
                Mp = 0,
                MaxMp = 1,
                Hand = new List<CardFromGameServerDto>{
                    new CardFromGameServerDto{
                        Id = 4,
                        Cost = 2,
                        Hp = 3,
                        Atk = 2,
                        Mechanics = new (),
                        Uid = 3,
                        BaseHP = 3
                    },
                    new CardFromGameServerDto{
                        Id = 22,
                        Cost = 7,
                        Hp = 7,
                        Atk = 7,
                        Mechanics = new (),
                        Uid = 5,
                        BaseHP = 7
                    },
                    new CardFromGameServerDto{
                        Id = 10,
                        Cost = 3,
                        Hp = 3,
                        Atk = 3,
                        Mechanics = new (){ "taunt", "charge" },
                        Uid = 6,
                        BaseHP = 3
                    }
                },
                Board = new List<CardFromGameServerDto>{
                    new CardFromGameServerDto{
                        Id = 2,
                        Cost = 1,
                        Hp = 1,
                        Atk = 2,
                        Mechanics = new (),
                        Uid = 7,
                        BaseHP = 1,
                        State = "SLEEP"
                    }
                },
                WelcomeText = "My life for Aiur!",
                HeroClass = "Warrior",
                RemainingCardsCount = 24,
                Opponent = new OpponentGameStateFromServerDto
                {
                    Username = "Dummy-AI",
                    HeroClass = "Hunter",
                    Hp = 30,
                    Mp = 0,
                    Board = new(),
                    WelcomeText = "Die, maggot!",
                    RemainingCardsCount = 24,
                    HandSize = 3
                },
                LatestActions = new()
            });

            var fakeTask = Task.FromResult(gameState);

            return await ProcessGameResponse(fakeTask);
        }

        public async Task<ServiceResponse<string>> JoinGameAsync(string playerKey, string type, string? mode, string? privateKey)
        {
            ServiceResponse<string> response = new();

            if (string.IsNullOrEmpty(type))
            {
                response.Success = false;
                return response;
            }

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

            ServerResponse<string> res = await GameServerAPI.CallApi<string>(GetUrl("auto-match"), data);

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
            Dictionary<string, string> data = new()
            {
                {"key", playerKey},
                {"username", username},
            };

            return await ProcessGameResponse(GameServerAPI.CallApi<GameStateFromServerDto>(GetUrl("observe"), data));
        }

        public async Task<ServiceResponse<bool>> SaveGameResultAsync(int playerId, string opponent, bool victory, Guid deckId, List<int> playedCardsIds)
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

            var savedGame = await _gameRepo.CreateGameAsync(game);

            await _gameRepo.AddPlayedCardsAsync(playerId, victory, playedCardsIds);

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

        public async Task<ServiceResponse<PaginatedResponse<GameResultDto>>> GetGamesHistoryAsync(int playerIdInt, int pageNumber, int pageSize)
        {
            var gameHistory = await _gameRepo.GetGamesHistoryAsync(playerIdInt, pageNumber, pageSize);

            var mappedGameHistory = _mapper.Map<PaginatedResponse<GameResultDto>>(gameHistory);

            ServiceResponse<PaginatedResponse<GameResultDto>> response = new()
            {
                Data = mappedGameHistory
            };

            if (mappedGameHistory == null)
            {
                response.Success = false;
                response.Message = "Player not found";
            }
            else if (!mappedGameHistory.Items.Any())
            {
                response.Message = "No games history available";
            }

            return response;
        }

        private string GetUrl(string service)
        {
            return _baseApiUrl + service;
        }

        private async Task<ServiceResponse<GameStateContainerDto>> ProcessGameResponse(
            Task<ServerResponse<GameStateFromServerDto>> gameResponseTask)
        {
            ServiceResponse<GameStateContainerDto> response = new();

            var res = await gameResponseTask;

            if (res.IsValid && res.Content != null)
            {
                var gameState = _mapper.Map<GameStateDto>(res.Content);
                response.Data = new GameStateContainerDto() { GameState = gameState };
            }
            else if (res.IsError && res.Error != null)
            {
                string errorWithoutQuotes = res.Error.Trim('\"');

                if (_validAnswers.Contains(errorWithoutQuotes))
                {
                    response.Data = new GameStateContainerDto() { Message = res.Error };
                }
                else
                {
                    response.Success = false;
                    response.Message = res.Error;
                }
            }

            return response;
        }
    }
}