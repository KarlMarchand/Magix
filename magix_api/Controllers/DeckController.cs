using Microsoft.AspNetCore.Mvc;
using magix_api.Services.DeckService;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.CardDto;
using Microsoft.AspNetCore.Authorization;
using magix_api.utils;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;

        public DeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        [HttpGet("Options/All")]
        public async Task<ActionResult<ServiceResponse<GetAvailableOptionsDto>>> GetAllOptions()
        {
            return Ok(await _deckService.GetAllOptions());
        }

        [HttpGet("Options/Cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            return Ok(await _deckService.GetAllCards());
        }

        [HttpGet("Options/Factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            return Ok(await _deckService.GetAllFactions());
        }

        [HttpGet("Options/Heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _deckService.GetAllHeroes());
        }

        [HttpGet("Options/Talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            return Ok(await _deckService.GetAllTalents());
        }

        [HttpGet("{deckId}")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<Deck>>>> GetDeck([FromRoute] int deckId)
        {
            return Ok(await _deckService.GetDeck(deckId));
        }

        [HttpGet("All")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<Deck>>>> GetAllDecks()
        {
            return Ok(await _deckService.GetAllDecks(User.GetPlayerId()));
        }

        [HttpPost("Switch")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<Deck>>> SwitchDeck(int deckId)
        {
            return Ok(await _deckService.SwitchDeck(User.GetPlayerKey(), deckId));
        }

        [HttpPost]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<Deck>>> CreateDeck(DeckDto deck)
        {
            return Ok(await _deckService.CreateDeck(User.GetPlayerKey(), User.GetPlayerId(), deck));
        }

        [HttpPut]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<Deck>>> UpdateDeck(Deck deck)
        {
            return Ok(await _deckService.UpdateDeck(User.GetPlayerKey(), deck));
        }

        [HttpDelete]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<Deck>>> DeleteDeck(Deck deck)
        {
            return Ok(await _deckService.DeleteDeck(User.GetPlayerKey(), User.GetPlayerId(), deck));
        }
    }
}