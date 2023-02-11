using AutoMapper;
using magix_api.utils;
using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        // private async Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(ServerPlayerDto newPlayer)
        // {
        //     var player = _mapper.Map<Player>(newPlayer);
        //     await _context.Players.AddAsync(player);
        //     var players = await _context.Players.ToListAsync();
        //     serviceResponse.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
        // }

        public async Task<ServiceResponse<GetPlayerDto>> Login(LoginPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            var response = await GameServerAPI.CallApi<ServerPlayerDto>("signin", new Dictionary<string, string>() {
                { "username", userInfos.Username },
                { "password", userInfos.Password }
            });
            if (response != null && response.GetType() != typeof(string))
            {
                GetPlayerDto player = await _playerRepository.GetPlayer(response);
                serviceResponse.Data = player;
            }
            else
            {
                throw new Exception("Bad Logins");
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetProfile(IdPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> Logout(IdPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<string>();
            var response = await GameServerAPI.CallApi<string>("signout", new Dictionary<string, string>() { { "key", userInfos.Key } });
            serviceResponse.Data = response;
            return serviceResponse;
        }
    }
}