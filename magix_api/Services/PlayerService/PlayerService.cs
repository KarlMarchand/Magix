using AutoMapper;
using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;
using magix_api.Services.AuthentificationService;
using magix_api.Services.DeckService;
using magix_api.utils;

namespace magix_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuthentificationService _authentificationService;
        private readonly IDeckService _deckService;

        public PlayerService(IAuthentificationService authentificationService, IMapper mapper, IPlayerRepository playerRepository, IDeckService deckService)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _authentificationService = authentificationService;
            _deckService = deckService;
        }

        public async Task<ServiceResponse<GetPlayerDto>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();

            ServerResponse<GameServerPlayerDto> response = await GameServerAPI.CallApi<GameServerPlayerDto>("signin", new Dictionary<string, string>() {
                { "username", username },
                { "password", password }
            });

            if (response.IsValid && response.Content != null)
            {
                // Create a player object from the GameServerAPI's return and add missing informations from the database
                Player playerToAuthenticate = _mapper.Map<Player>(response.Content);
                playerToAuthenticate.Username = username;
                Player player = await _playerRepository.GetPlayer(playerToAuthenticate);

                GetPlayerDto playerOutput = _mapper.Map<GetPlayerDto>(player);
                playerOutput.Token = _authentificationService.GenerateJwtToken(player);

                // Set the active deck of the player
                var activeDeckResult = await _deckService.GetActiveDeck(player.Id, player.Key);
                playerOutput.ActiveDeck = activeDeckResult.Success ? activeDeckResult.Data : null;

                serviceResponse.Data = playerOutput;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Bad Logins";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerStatsDto>> GetProfile(int playerId)
        {
            var serviceResponse = new ServiceResponse<GetPlayerStatsDto>();

            PlayerStat playerStats = await _playerRepository.GetPlayerStats(playerId);

            if (playerStats != null)
            {
                serviceResponse.Data = _mapper.Map<GetPlayerStatsDto>(playerStats);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Couldn't find the player";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Logout(string playerKey)
        {
            var serviceResponse = new ServiceResponse<string>();
            ServerResponse<string> response = await GameServerAPI.CallApi<string>("signout", new Dictionary<string, string>() { { "key", playerKey } });

            if (response.IsValid)
            {
                serviceResponse.Data = response.Content;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "A problem occurred with the Game Server API";
            }

            return serviceResponse;
        }
    }
}