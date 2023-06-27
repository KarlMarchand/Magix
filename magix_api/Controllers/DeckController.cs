using Microsoft.AspNetCore.Mvc;
using magix_api.Services.DeckService;
using magix_api.Services.GameOptionsService;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.CardDto;
using magix_api.Middlewares;
using magix_api.Dtos.PlayerDto;

namespace magix_api.Controllers
{
    [ApiController]
    [Route("api/deck")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckService _deckService;
        private readonly IGameOptionsService _gameOptionsService;

        public DeckController(IDeckService deckService, IGameOptionsService gameOptions)
        {
            _deckService = deckService;
            _gameOptionsService = gameOptions;
        }

        [HttpGet("options/all")]
        public async Task<ActionResult<ServiceResponse<GetAvailableOptionsDto>>> GetAllOptions()
        {
            return Ok(await _gameOptionsService.GetAllOptions());
        }

        [HttpGet("options/cards")]
        public async Task<ActionResult<ServiceResponse<List<GetCardDto>>>> GetAllCards()
        {
            return Ok(await _gameOptionsService.GetAllCards());
        }

        [HttpGet("options/factions")]
        public async Task<ActionResult<ServiceResponse<List<GetFactionDto>>>> GetAllFactions()
        {
            return Ok(await _gameOptionsService.GetAllFactions());
        }

        [HttpGet("options/heroes")]
        public async Task<ActionResult<ServiceResponse<List<GetHeroDto>>>> GetAllHeroes()
        {
            return Ok(await _gameOptionsService.GetAllHeroes());
        }

        [HttpGet("options/talents")]
        public async Task<ActionResult<ServiceResponse<List<GetTalentDto>>>> GetAllTalents()
        {
            return Ok(await _gameOptionsService.GetAllTalents());
        }

        [ValidateKey]
        [HttpPost("all")]
        public async Task<ActionResult<ServiceResponse<List<Deck>>>> GetAllDecks(IdPlayerDto playerInfos)
        {
            return Ok(await _deckService.GetAllDecks(playerInfos));
        }

        [ValidateKey]
        [HttpPost("select/{id}")]
        public async Task<ActionResult<ServiceResponse<Deck>>> SwitchDeck([FromRoute] int id, IdPlayerDto playerInfos)
        {
            return Ok(await _deckService.SwitchDeck(playerInfos, id));
        }

        [ValidateKey]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Deck>>> CreateDeck(IdPlayerDto playerInfos, Deck deck)
        {
            return Ok(await _deckService.CreateDeck(playerInfos, deck));
        }

        [ValidateKey]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Deck>>> UpdateDeck(IdPlayerDto playerInfos, Deck deck)
        {
            return Ok(await _deckService.UpdateDeck(playerInfos, deck));
        }

        [ValidateKey]
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<Deck>>> DeleteDeck(IdPlayerDto playerInfos, Deck deck)
        {
            return Ok(await _deckService.DeleteDeck(playerInfos, deck));
        }
    }
}