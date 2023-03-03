using magix_api.Dtos.PlayerDto;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        public async Task<ServiceResponse<Deck>> AddDeck(IdPlayerDto playerInfos, Deck deck)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Deck>> DeleteDeck(IdPlayerDto playerInfos, Deck deck)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Deck>>> GetAllDecks(IdPlayerDto playerInfos)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Deck>> SaveDeck(IdPlayerDto playerInfos, Deck deck)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Deck>> SwitchDeck(int id, IdPlayerDto playerInfos)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Deck>> GetDeck(int id, IdPlayerDto playerInfos)
        {
            throw new NotImplementedException();
        }
    }
}