using magix_api.Dtos.PlayerDto;
using magix_api.Repositories;
using magix_api.utils;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepo;

        public DeckService(IDeckRepository deckRepo)
        {
            _deckRepo = deckRepo;
        }

        public async Task<ServiceResponse<Deck>> CreateDeck(IdPlayerDto playerInfos, Deck deck)
        {
            ServiceResponse<Deck> response = new();
            var success = await _SendDeckServer(playerInfos, deck);
            if (success)
            {
                response.Data = await _deckRepo.CreateDeck(deck);
                response.Success = response.Data != null;
            }
            return response;
        }

        public async Task<ServiceResponse<Deck>> DeleteDeck(IdPlayerDto playerInfos, Deck deck)
        {
            ServiceResponse<Deck> response = new();
            response.Success = await _deckRepo.DeleteDeck(deck);
            if (response.Success)
            {
                var newDeck = (await _deckRepo.GetAllDecks(playerInfos.Id)).FirstOrDefault();
                if (newDeck != null)
                {
                    var success = await _SendDeckServer(playerInfos, newDeck);
                    if (success)
                    {
                        response.Data = newDeck;
                    }
                }
            }
            return response;
        }

        public async Task<ServiceResponse<List<Deck>>> GetAllDecks(IdPlayerDto playerInfos)
        {
            ServiceResponse<List<Deck>> response = new();
            response.Data = await _deckRepo.GetAllDecks(playerInfos.Id);
            return response;
        }

        public async Task<ServiceResponse<Deck>> UpdateDeck(IdPlayerDto playerInfos, Deck deck)
        {
            ServiceResponse<Deck> response = new();

            var successfullySent = await _SendDeckServer(playerInfos, deck);

            if (successfullySent)
            {
                response.Data = await _deckRepo.UpdateDeck(deck);
                response.Success = response.Data != null;
            }

            return response;
        }

        public async Task<ServiceResponse<Deck>> SwitchDeck(IdPlayerDto playerInfos, int id)
        {
            ServiceResponse<Deck> response = new();

            var deck = await _deckRepo.GetDeck(id);

            var successfullySent = await _SendDeckServer(playerInfos, deck);
            if (successfullySent)
            {
                response.Data = deck;
                response.Success = response.Data != null;
            }

            return response;
        }

        public async Task<ServiceResponse<Deck>> GetDeck(int id)
        {
            ServiceResponse<Deck> response = new();
            response.Data = await _deckRepo.GetDeck(id);
            return response;
        }

        private async Task<Boolean> _SendDeckServer(IdPlayerDto playerInfos, Deck deck)
        {
            var success = false;

            string apiUrl = "/api/users/deck/save";

            Dictionary<string, string> data = new()
            {
                {"key", playerInfos.Key},
                {"deck", deck.Cards.ToString()!},
                {"className", deck.Hero.ToServer()},
                {"initialTalent", deck.Talent.ToServer()}
            };

            ServerResponse<bool> res = await GameServerAPI.CallApi<bool>(apiUrl, data);

            if (res.IsValid && res.Content)
            {
                success = true;
            }
            return success;
        }
    }
}