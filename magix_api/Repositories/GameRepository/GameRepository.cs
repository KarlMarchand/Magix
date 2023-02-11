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
    }
}