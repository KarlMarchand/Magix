using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;

namespace magix_api.Services.DeckService
{
    public interface IDeckService
    {
        Task<ServiceResponse<GetDeckDto>> GetDeck(Guid id);
        Task<ServiceResponse<GetDeckDto>> GetActiveDeck(int playerId, string? playerKey = null);
        Task<ServiceResponse<List<GetDeckDto>>> GetAllDecks(int playerId);
        Task<ServiceResponse<GetDeckDto>> SwitchDeck(string playerKey, int playerId, Guid deckId);
        Task<ServiceResponse<GetDeckDto>> CreateDeck(string playerKey, int playerId, CreateDeckDto deck);
        Task<ServiceResponse<GetDeckDto>> UpdateDeck(string playerKey, int playerId, CreateDeckDto deck);
        Task<ServiceResponse<GetDeckDto>> DeleteDeck(string playerKey, int playerId, Guid deckId);
        Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions();
        Task<ServiceResponse<List<GetCardDto>>> GetAllCards();
        Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions();
        Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes();
        Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents();
    }
}