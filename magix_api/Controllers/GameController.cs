using magix_api.Dtos;
using magix_api.Dtos.GameDto;
using magix_api.Services.GameService;
using magix_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("join")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> JoinGameAsync([FromBody] JoinGameDto gameInfos)
        {
            var response = await _gameService.JoinGameAsync(User.GetPlayerKey(), gameInfos.Type, gameInfos.Mode, gameInfos.PrivateKey);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GameActionAsync([FromBody] GameActionDto gameAction)
        {
            var response = await _gameService.GameActionAsync(User.GetPlayerKey(), gameAction);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<GameStateContainerDto>>> GetGameStateAsync()
        {
            var response = await _gameService.GetGameStateAsync(User.GetPlayerKey());
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("observe/{username}")]
        [Authorize]
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

        [HttpGet("history")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<PaginatedResponse<GameResultDto>>>> GetGameHistoryAsync(
            [FromQuery] string? playerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 30)
        {
            var playerIdInt = string.IsNullOrWhiteSpace(playerId) ? User.GetPlayerId() : int.Parse(playerId);
            var response = await _gameService.GetGamesHistoryAsync(playerIdInt, pageNumber, pageSize);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}