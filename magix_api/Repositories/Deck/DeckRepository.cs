using magix_api.utils;
using magix_api.Data;
using Microsoft.EntityFrameworkCore;

namespace magix_api.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly MagixContext _context;

        public DeckRepository(MagixContext context)
        {
            _context = context;
        }

        public async Task<Deck> AddDeck(int playerId, Deck deck)
        {
            throw new NotImplementedException();
        }

        public async Task<Deck> DeleteDeck(int playerId, Deck deck)
        {
            throw new NotImplementedException();
        }

        public List<Deck> GetAllDecks(int playerId)
        {
            List<Deck> decks = _context.Decks
                .Include(d => d.Player)
                .Where(d => d.Player.Id == playerId)
                .Include(d => d.DeckCards)
                    .ThenInclude(dc => dc.Card)
                .ToList();

            foreach (var deck in decks)
            {
                deck.Cards = deck.DeckCards
                    .SelectMany(dc => Enumerable.Repeat(dc.Card, dc.Quantity))
                    .ToList();
            }

            return decks;
        }

        public async Task<Deck?> GetDeck(int deckId)
        {
            var deck = await _context.Decks
                .Include(p => p.DeckCards)
                    .ThenInclude(p => p.Card)
                .SingleOrDefaultAsync(p => p.Id == deckId);

            if (deck != null)
            {
                deck.Cards = deck.DeckCards
                    .SelectMany(dc => Enumerable.Repeat(dc.Card, dc.Quantity))
                    .ToList();
            }

            return deck;
        }

        public async Task<Deck> SaveDeck(string playerKey, int playerId, Deck deck)
        {
            string apiUrl = "/api/users/deck/save";

            Dictionary<string, string> data = new()
            {
                {"key", playerKey},
                {"deck", deck.Cards.ToString()!},
                {"className", deck.Hero.ToServer()},
                {"initialTalent", deck.Talent.ToServer()},
            };

            ServerResponse<bool> response = await GameServerAPI.CallApi<bool>(apiUrl, data);
            if (response.IsValid && response.Content)
            {
                _context.Decks.Add(deck);
                await _context.SaveChangesAsync();
            }
            return deck;
        }

        public async Task<Deck> SwitchDeck(int id, int playerId)
        {
            throw new NotImplementedException();
        }
    }

}