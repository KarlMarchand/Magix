using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.DeckService
{
    public interface IDeckService
    {
        Task<ServiceResponse<List<Deck>>> GetAllDecks(IdPlayerDto playerInfos);
    }
}