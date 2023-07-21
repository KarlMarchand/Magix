using Microsoft.AspNetCore.Mvc;
using magix_api.Services.GameService;
using magix_api.Dtos.GameDto;
using Microsoft.AspNetCore.Authorization;
using magix_api.utils;

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

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> JoinGameAsync(string type, string? mode, string? privateKey)
        {
            return Ok(await _gameService.JoinGameAsync(User.GetPlayerKey(), type, mode, privateKey));
        }

        [HttpPost("action")]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GameActionAsync(GameActionDto gameAction)
        {
            return Ok(await _gameService.GameActionAsync(User.GetPlayerKey(), gameAction));
        }

        [HttpGet("state")]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GetGameStateAsync()
        {
            return Ok(await _gameService.GetGameStateAsync(User.GetPlayerKey()));
        }

        [HttpPost("observe")]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> ObserveGameAsync(string username)
        {
            return Ok(await _gameService.ObserveGameAsync(User.GetPlayerKey(), username));
        }

        [HttpPost("save")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<bool>>> SaveGameResultAsync(string opponent, bool victory, int deckId)
        {
            return Ok(await _gameService.SaveGameResultAsync(User.GetPlayerId(), opponent, victory, deckId));
        }
    }
}