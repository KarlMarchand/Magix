using AutoMapper;
using magix_api.utils;
using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;

namespace magix_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IMapper mapper, IPlayerRepository playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public async Task<ServiceResponse<GetPlayerDto>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            ServerResponse<Player> response = await GameServerAPI.CallApi<Player>("signin", new Dictionary<string, string>() {
                { "username", username },
                { "password", password }
            });
            if (response.IsValid && response.Content != null)
            {
                Player incompletePlayer = response.Content;
                incompletePlayer.Username = username;
                Player player = await _playerRepository.GetPlayer(incompletePlayer);
                serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Bad Logins";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetProfile(IdPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            Player player = await _playerRepository.GetCompleteProfile(userInfos.Id);
            serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Logout(IdPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<string>();
            ServerResponse<string> response = await GameServerAPI.CallApi<string>("signout", new Dictionary<string, string>() { { "key", userInfos.Key } });
            serviceResponse.Data = response.Content;
            return serviceResponse;
        }
    }
}