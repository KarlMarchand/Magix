using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.DeckService
{
    public interface IDeckService
    {
        Task<ServiceResponse<Deck>> GetDeck(int id);
        Task<ServiceResponse<List<Deck>>> GetAllDecks(IdPlayerDto playerInfos);
        Task<ServiceResponse<Deck>> SwitchDeck(IdPlayerDto playerInfos, int id);
        Task<ServiceResponse<Deck>> CreateDeck(IdPlayerDto playerInfos, Deck deck);
        Task<ServiceResponse<Deck>> UpdateDeck(IdPlayerDto playerInfos, Deck deck);
        Task<ServiceResponse<Deck>> DeleteDeck(IdPlayerDto playerInfos, Deck deck);
    }
}