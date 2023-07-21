using magix_api.Data;

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
    }
}