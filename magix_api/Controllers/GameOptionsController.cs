using Microsoft.AspNetCore.Mvc;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.CardDto;
using magix_api.Services.GameOptionsService;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameOptionsController : ControllerBase
    {
        private readonly IGameOptionsService _gameOptionsService;
        public GameOptionsController(IGameOptionsService gameOptions)
        {
            _gameOptionsService = gameOptions;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<GetAvailableOptionsDto>>> GetAllOptions()
        {
            return Ok(await _gameOptionsService.GetAllOptions());
        }

        [HttpGet("Cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            return Ok(await _gameOptionsService.GetAllCards());
        }

        [HttpGet("Factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            return Ok(await _gameOptionsService.GetAllFactions());
        }

        [HttpGet("Heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _gameOptionsService.GetAllHeroes());
        }

        [HttpGet("Talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            return Ok(await _gameOptionsService.GetAllTalents());
        }

    }
}