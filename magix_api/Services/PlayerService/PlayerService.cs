using AutoMapper;
using Microsoft.EntityFrameworkCore;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;

        public PlayerService(IMapper mapper)
        {
            _mapper = mapper;
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

            // TODO: send infos to game server for verification
            // var response = await GameServerAPI.CallApi<>(userInfos.Username, userInfos.Password)

            // var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == userInfos.Username);
            // if (player is null)
            // {
            //     // TODO: Should create a new player

            // }
            //_mapper.Map<GetPlayerDto>(player);

            var data = new GetPlayerDto();
            data.Username = userInfos.Username;

            serviceResponse.Data = data;
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
            serviceResponse.Data = "Disconnected";
            return serviceResponse;
        }
    }
}