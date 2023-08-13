using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Services.DeckService;
using magix_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("options/all")]
        public async Task<ActionResult<ServiceResponse<GetAvailableOptionsDto>>> GetAllOptions()
        {
            return Ok(await _deckService.GetAllOptions());
        }

        [HttpGet("options/cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            return Ok(await _deckService.GetAllCards());
        }

        [HttpGet("options/factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            return Ok(await _deckService.GetAllFactions());
        }

        [HttpGet("options/heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _deckService.GetAllHeroes());
        }

        [HttpGet("options/talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            return Ok(await _deckService.GetAllTalents());
        }

        [HttpGet("{deckId}")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetDeck([FromRoute] Guid deckId)
        {
            return Ok(await _deckService.GetDeck(deckId));
        }

        [HttpGet("all")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetAllDecks()
        {
            return Ok(await _deckService.GetAllDecks(User.GetPlayerId()));
        }

        [HttpGet("active")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetActiveDeck()
        {
            return Ok(await _deckService.GetActiveDeck(User.GetPlayerId()));
        }

        [HttpPost("switch")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> SwitchDeck(Guid deckId)
        {
            return Ok(await _deckService.SwitchDeck(User.GetPlayerKey(), User.GetPlayerId(), deckId));
        }

        [HttpPost]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> CreateDeck(DeckDto deck)
        {
            return Ok(await _deckService.CreateDeck(User.GetPlayerKey(), User.GetPlayerId(), deck));
        }

        [HttpPut]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> UpdateDeck(DeckDto deck)
        {
            try
            {
                return Ok(await _deckService.UpdateDeck(User.GetPlayerKey(), User.GetPlayerId(), deck));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> DeleteDeck(Guid deckId)
        {
            return Ok(await _deckService.DeleteDeck(User.GetPlayerKey(), User.GetPlayerId(), deckId));
        }
    }
}