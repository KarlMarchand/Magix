using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepo;

        public DeckService(IDeckRepository deckRepo)
        {
            _deckRepo = deckRepo;
        }

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
            ServiceResponse<Deck> response = new();
            response.Data = await _deckRepo.SaveDeck(playerInfos.Key, playerInfos.Id, deck);
            if (response.Data == null)
            {
                response.Success = false;
            }
            return response;
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