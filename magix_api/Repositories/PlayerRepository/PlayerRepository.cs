using magix_api.Data;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MagixContext _context;

        public PlayerRepository(MagixContext context)
        {
            _context = context;
        }

        Task<GetPlayerDto> IPlayerRepository.GetPlayer(ServerPlayerDto playerInfos)
        {
            throw new NotImplementedException();
        }
    }
}