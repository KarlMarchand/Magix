using magix_api.Dtos.PlayerDto;

namespace magix_api.Repositories
{
    public interface IPlayerRepository
    {
        Task<GetPlayerDto> GetPlayer(ServerPlayerDto playerInfos);
    }
}