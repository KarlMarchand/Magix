using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magix_api.Dtos.Player;
using magix_api.Models;


namespace magix_api.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<List<GetPlayerDto>>> GetAllPlayers();
        Task<ServiceResponse<GetPlayerDto>> GetPlayerByUsername(string username);
        Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer);
        Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer);
        Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id);
    }
}