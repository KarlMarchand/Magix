using magix_api.Dtos.PlayerDto;
using magix_api.Services.PlayerService;
using magix_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> Login([FromBody] LoginDto loginInformations)
        {
            var response = await _playerService.Login(loginInformations.Username, loginInformations.Password);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> Logout()
        {
            return Ok((await _playerService.Logout(User.GetPlayerKey())));
        }

        [HttpGet]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetPlayerStatsDto>>> GetProfile()
        {
            var response = await _playerService.GetProfile(User.GetPlayerId());
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}