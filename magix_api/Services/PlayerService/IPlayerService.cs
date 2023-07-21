using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<GetPlayerDto>> Login(string username, string password);
        Task<ServiceResponse<string>> Logout(string playerKey);
        Task<ServiceResponse<GetPlayerStatsDto>> GetProfile(int playerId);
    }
}