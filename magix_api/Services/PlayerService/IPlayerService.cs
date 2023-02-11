using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<GetPlayerDto>> Login(LoginPlayerDto userInfos);
        Task<ServiceResponse<string>> Logout(IdPlayerDto userInfos);
        Task<ServiceResponse<GetPlayerDto>> GetProfile(IdPlayerDto userInfos);
    }
}