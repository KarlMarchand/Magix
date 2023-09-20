using magix_api.Dtos.GameDto;
using magix_api.Services.GameService;
using magix_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/game")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("join")]
        public async Task<ActionResult<ServiceResponse<string>>> JoinGameAsync([FromBody] JoinGameDto gameInfos)
        {
            var response = await _gameService.JoinGameAsync(User.GetPlayerKey(), gameInfos.Type, gameInfos.Mode, gameInfos.PrivateKey);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GameActionAsync([FromBody] GameActionDto gameAction)
        {
            var response = await _gameService.GameActionAsync(User.GetPlayerKey(), gameAction);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GetGameStateAsync()
        {
            var response = await _gameService.GetGameStateAsync(User.GetPlayerKey());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("observe/{username}")]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> ObserveGameAsync(string username)
        {
            var response = await _gameService.ObserveGameAsync(User.GetPlayerKey(), username);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("save")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<bool>>> SaveGameResultAsync([FromBody] SaveGameDto gameToSave)
        {
            var response = await _gameService.SaveGameResultAsync(User.GetPlayerId(), gameToSave.Opponent, gameToSave.Victory, gameToSave.DeckId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}