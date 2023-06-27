using Microsoft.AspNetCore.Mvc;
using magix_api.Services.GameService;
using magix_api.Middlewares;

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

        // [ValidateKey]
        // [HttpPost("observe")]
        // public async Task<ActionResult<ServiceResponse>>

    }
}