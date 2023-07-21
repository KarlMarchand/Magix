using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;

namespace magix_api.Services.DeckService
{
    public interface IDeckService
    {
        Task<ServiceResponse<Deck>> GetDeck(int id);
        Task<ServiceResponse<List<Deck>>> GetAllDecks(int playerId);
        Task<ServiceResponse<Deck>> SwitchDeck(string playerKey, int deckId);
        Task<ServiceResponse<Deck>> CreateDeck(string playerKey, int playerId, DeckDto deck);
        Task<ServiceResponse<Deck>> UpdateDeck(string playerKey, Deck deck);
        Task<ServiceResponse<Deck>> DeleteDeck(string playerKey, int playerId, Deck deck);
        Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions();
        Task<ServiceResponse<List<GetCardDto>>> GetAllCards();
        Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions();
        Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes();
        Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents();
    }
}