using magix_api.Data;
using magix_api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace magix_api.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly MagixContext _context;

        public GameRepository(MagixContext context)
        {
            _context = context;
        }

        public async Task<Game> CreateGame(Game game)
        {
            var entry = await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<PaginatedResponse<Game>> GetGamesHistoryAsync(int playerId, int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            // Fetch the total number of items
            int totalItems = await _context.Games.CountAsync(game => game.PlayerId == playerId);

            // Retrieve the paginated data
            var games = await _context.Games
                                      .Where(game => game.PlayerId == playerId)
                                      .OrderByDescending(game => game.Date)
                                      .Skip(skip)
                                      .Take(pageSize)
                                      .ToListAsync();

            return new PaginatedResponse<Game>(games, totalItems, pageNumber, pageSize);
        }
    }
}