using magix_api.Dtos.Player;

namespace magix_api.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<List<GetPlayerDto>>> GetAllPlayers();
        Task<ServiceResponse<GetPlayerDto>> GetPlayerByUsername(string username);
        Task<ServiceResponse<GetPlayerDto>> Login(LoginPlayerDto userInfos);
        Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer);
        Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer);
        Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id);
    }
}