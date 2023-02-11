using magix_api.Data;

namespace magix_api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MagixContext _context;

        public PlayerRepository(MagixContext context)
        {
            _context = context;
        }
    }
}