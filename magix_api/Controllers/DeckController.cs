using Microsoft.AspNetCore.Mvc;
using magix_api.Services.DeckService;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.CardDto;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ServiceResponse<GetAvailableOptionsDto>>> GetAllOptions()
        {
            return Ok(await _deckService.GetAllOptions());
        }

        [HttpGet("Cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            return Ok(await _deckService.GetAllCards());
        }

        [HttpGet("Factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            return Ok(await _deckService.GetAllFactions());
        }

        [HttpGet("Heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _deckService.GetAllHeroes());
        }

        [HttpGet("Talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            return Ok(await _deckService.GetAllTalents());
        }

    }
}