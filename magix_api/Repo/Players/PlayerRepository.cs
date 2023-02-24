using magix_api.Data;
using magix_api.Dtos.PlayerDto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace magix_api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MagixContext _context;
        private readonly IMapper _mapper;

        public PlayerRepository(MagixContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> GetPlayer(Player serverPlayer)
        {
            // The serverPlayer only has the key, trophies and bestTrophyScore property of the player which must be then completed by the repository
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == serverPlayer.Username);
            if (player is null)
            {
                player = await AddPlayer(serverPlayer.Username, serverPlayer.Trophies, serverPlayer.BestTrophyScore);
            }
            else
            {
                player.Trophies = serverPlayer.Trophies;
                player.BestTrophyScore = serverPlayer.BestTrophyScore;
                _context.SaveChanges();

            }
            // Complete info with player games stats in database
            if (player != null)
            {
                // PlayerStat? stats = await _context.PlayerStats.FirstOrDefaultAsync(p => p.Id == player.Id);
            }
            return player;
        }

        public async Task<Player> AddPlayer(string username, int trophies, int bestTrophyScore)
        {
            Player newPlayer = new Player{Username=username, Trophies = trophies, BestTrophyScore=bestTrophyScore};
            await _context.Players.AddAsync(newPlayer);
            Player? player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
            return player!;
        }

        public async Task<Player> GetProfile(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}