using magix_api.Data;
using Microsoft.EntityFrameworkCore;

namespace magix_api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MagixContext _context;

        public PlayerRepository(MagixContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayer(Player apiPlayer)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == apiPlayer.Username);
            player = player is null ? await AddPlayer(apiPlayer) : await UpdatePlayer(player.Id, apiPlayer);
            player.Key = apiPlayer.Key;
            return player;
        }

        public async Task<Player> AddPlayer(Player newPlayer)
        {
            await _context.Players.AddAsync(newPlayer);
            await _context.SaveChangesAsync();
            return newPlayer;
        }

        public async Task<Player> UpdatePlayer(int playerId, Player apiPlayer)
        {
            var player = await _context.Players.SingleAsync(p => p.Id == playerId);
            player.WinCount = apiPlayer.WinCount;
            player.LossCount = apiPlayer.LossCount;
            player.LastLogin = apiPlayer.LastLogin;
            player.WelcomeText = apiPlayer.WelcomeText;
            player.Trophies = apiPlayer.Trophies;
            player.BestTrophyScore = apiPlayer.BestTrophyScore;
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<PlayerStat> GetPlayerStats(int playerId)
        {
            Player player = await _context.Players.Where(p => p.Id == playerId)
                                                    .Include(p => p.Games)
                                                    .Include(p => p.PlayedCards)
                                                        .ThenInclude(pc => pc.Card)
                                                    .SingleAsync();
            return new PlayerStat
            {
                GamePlayed = player.Games.Count,
                Wins = player.WinCount,
                Loses = player.LossCount,
                RatioWins = player.Games.Any() ? Math.Round((decimal)player.WinCount / player.Games.Count, 2) : null,
                TopCards = player.PlayedCards
                                .Where(pc => pc.Card != null)
                                .GroupBy(pc => pc.Card!)
                                .Select(g => new { Card = g.Key, VictoryCount = g.Sum(pc => pc.Victory) })
                                .OrderByDescending(x => x.VictoryCount)
                                .Take(3)
                                .Select(x => x.Card)
                                .ToList()
            };
        }
    }
}