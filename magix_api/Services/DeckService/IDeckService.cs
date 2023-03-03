using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.DeckService
{
    public interface IDeckService
    {
        Task<ServiceResponse<List<Deck>>> GetAllDecks(IdPlayerDto playerInfos);
        Task<ServiceResponse<Deck>> SwitchDeck(int id, IdPlayerDto playerInfos);
        Task<ServiceResponse<Deck>> AddDeck(IdPlayerDto playerInfos, Deck deck);
        Task<ServiceResponse<Deck>> SaveDeck(IdPlayerDto playerInfos, Deck deck);
        Task<ServiceResponse<Deck>> DeleteDeck(IdPlayerDto playerInfos, Deck deck);
    }
}