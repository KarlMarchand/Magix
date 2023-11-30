using magix_api.Custom_Exceptions;
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
            var response = await _deckService.GetAllOptions();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("options/cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            var response = await _deckService.GetAllCards();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("options/factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            var response = await _deckService.GetAllFactions();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("options/heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            var response = await _deckService.GetAllHeroes();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("options/talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            var response = await _deckService.GetAllTalents();
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("{deckId}")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetDeck([FromRoute] Guid deckId)
        {
            var response = await _deckService.GetDeck(deckId);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("all")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetAllDecks()
        {
            var response = await _deckService.GetAllDecks(User.GetPlayerId());
            return response.Success ? Ok(response) : NotFound(response);

        }

        [HttpGet("active")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<List<GetDeckDto>>>> GetActiveDeck()
        {
            var response = await _deckService.GetActiveDeck(User.GetPlayerId());
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost("switch/{deckId}")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> SwitchDeck([FromRoute] Guid deckId)
        {
            var response = await _deckService.SwitchDeck(User.GetPlayerKey(), User.GetPlayerId(), deckId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> CreateDeck(CreateDeckDto deck)
        {
            var response = await _deckService.CreateDeck(User.GetPlayerKey(), User.GetPlayerId(), deck);
            return response.Success ? Created(string.Empty, response) : BadRequest(response);
        }

        [HttpPut]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> UpdateDeck(CreateDeckDto deck)
        {
            try
            {
                var response = await _deckService.UpdateDeck(User.GetPlayerKey(), User.GetPlayerId(), deck);
                return Ok(response);
            }
            catch (ResourceUnauthorizedException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{deckId}")]
        [Authorize(Policy = "ValidateKey")]
        public async Task<ActionResult<ServiceResponse<GetDeckDto>>> DeleteDeck([FromRoute] Guid deckId)
        {
            var response = await _deckService.DeleteDeck(User.GetPlayerKey(), User.GetPlayerId(), deckId);
            return response.Success ? Ok() : NotFound(response);
        }
    }
}