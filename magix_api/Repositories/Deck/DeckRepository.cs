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
                .ToList();

            return decks;
        }

        public async Task<Deck> GetDeck(int deckId)
        {
            Deck deck = await _context.Decks
                .Include(p => p.DeckCards)
                    .ThenInclude(p => p.Card)
                .SingleAsync(p => p.Id == deckId);

            deck.Cards = new List<Card>();

            foreach (var deckCard in deck.DeckCards)
            {
                for (int i = 0; i < deckCard.Quantity; i++)
                {
                    deck.Cards.Add(deckCard.Card);
                }
            }

            return deck;
        }

        public async Task<Deck> SaveDeck(int playerId, Deck deck)
        {
            throw new NotImplementedException();
        }

        public async Task<Deck> SwitchDeck(int id, int playerId)
        {
            throw new NotImplementedException();
        }
    }

}