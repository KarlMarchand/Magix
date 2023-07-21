using magix_api.Dtos.PlayerDto;
using magix_api.Services.PlayerService;
using Microsoft.AspNetCore.Mvc;
using magix_api.utils;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace magix_api.Controllers
{
    [ApiController] //This means this is an API so it implements all the api's tools
    [Route("api/player")] // This means the controller can be accessed at the route api/The_name_of_this_controller
    public class PlayerController : ControllerBase //It must extends the ControllerBase's class from MVC 
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> Login(string username, [DataType(DataType.Password)] string password)
        {
            return Ok(await _playerService.Login(username, password));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> Logout()
        {
            return Ok(await _playerService.Logout(User.GetPlayerKey()));
        }

        [HttpGet]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetPlayerStatsDto>>> GetProfile()
        {
            return Ok(await _playerService.GetProfile(User.GetPlayerId()));
        }
    }
}