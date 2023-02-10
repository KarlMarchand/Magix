using AutoMapper;
using Microsoft.EntityFrameworkCore;
using magix_api.Data;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly MagixContext _context;

        public PlayerService(IMapper mapper, MagixContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer)
        {
            var serviceResponse = new ServiceResponse<List<GetPlayerDto>>();
            var player = _mapper.Map<Player>(newPlayer);
            await _context.Players.AddAsync(player);
            var players = await _context.Players.ToListAsync();
            serviceResponse.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> GetAllPlayers()
        {
            var serviceResponse = new ServiceResponse<List<GetPlayerDto>>();
            var players = await _context.Players.ToListAsync();
            serviceResponse.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerByUsername(string username)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
            serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == updatedPlayer.Id);

                if (player is null)
                {
                    throw new Exception($"Player with Id '{updatedPlayer.Id}' not found.");
                }

                _mapper.Map(updatedPlayer, player);

                /* vvv Equivalent to this vvv
                    player.Username = updatedPlayer.Username;
                    player.Class = updatedPlayer.Class;
                    player.Trophies = updatedPlayer.Trophies;
                    player.BestTrophyScore = updatedPlayer.BestTrophyScore;
                    player.Faction = updatedPlayer.Faction;
                */
                serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPlayerDto>>();
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id); //Function players.First will throw an error of its own if null but FirstOrDefault can return null

                if (player is null)
                {
                    throw new Exception($"Player with Id '{id}' not found.");
                }

                _context.Players.Remove(player);

                var players = await _context.Players.ToListAsync();
                serviceResponse.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> Login(LoginPlayerDto userInfos)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();

            // TODO: send infos to game server for verification
            // var response = call_magix_api(userInfos.Username, userInfos.Password)

            var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == userInfos.Username);
            if (player is null)
            {
                // TODO: Should create a new player

            }

            serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            return serviceResponse;
        }
    }
}