using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {

        Task<ServiceResponse<List<Deck>>> IDeckService.GetAllDecks(IdPlayerDto playerInfos)
        {
            throw new NotImplementedException();
        }
    }
}