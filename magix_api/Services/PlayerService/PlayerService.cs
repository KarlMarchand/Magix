using AutoMapper;
using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;
using magix_api.Services.AuthentificationService;
using magix_api.utils;

namespace magix_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuthentificationService _authentificationService;

        public PlayerService(IAuthentificationService authentificationService, IMapper mapper, IPlayerRepository playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _authentificationService = authentificationService;
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
                Player incompletePlayer = _mapper.Map<Player>(response.Content);
                incompletePlayer.Username = username;
                Player player = await _playerRepository.GetPlayer(incompletePlayer);
                GetPlayerDto playerOutput = _mapper.Map<GetPlayerDto>(player);
                playerOutput.Jwt = _authentificationService.GenerateJwtToken(player);
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
            serviceResponse.Data = _mapper.Map<GetPlayerStatsDto>(playerStats);
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Logout(string playerKey)
        {
            var serviceResponse = new ServiceResponse<string>();
            ServerResponse<string> response = await GameServerAPI.CallApi<string>("signout", new Dictionary<string, string>() { { "key", playerKey } });
            serviceResponse.Data = response.Content;
            return serviceResponse;
        }
    }
}