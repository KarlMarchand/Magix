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

        public async Task<Deck> CreateDeck(Deck deck)
        {
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return deck;
        }

        public async Task<bool> DeleteDeck(Deck deck)
        {
            return (await _context.Decks.Where(d => d.Id == deck.Id).ExecuteDeleteAsync()) > 0;
        }

        public async Task<List<Deck>> GetAllDecks(int playerId)
        {
            List<Deck> decks = await _context.Decks
                .Include(d => d.Player)
                .Where(d => d.Player.Id == playerId)
                .Include(d => d.DeckCards)
                    .ThenInclude(dc => dc.Card)
                .ToListAsync();

            foreach (var deck in decks)
            {
                deck.Cards = deck.DeckCards
                    .SelectMany(dc => Enumerable.Repeat(dc.Card, dc.Quantity))
                    .ToList();
            }

            return decks;
        }

        public async Task<Deck> GetDeck(int deckId)
        {
            var deck = await _context.Decks
                .Include(p => p.DeckCards)
                    .ThenInclude(p => p.Card)
                .SingleAsync(p => p.Id == deckId);

            deck.Cards = deck.DeckCards
                .SelectMany(dc => Enumerable.Repeat(dc.Card, dc.Quantity))
                .ToList();

            return deck;
        }

        public async Task<Deck> UpdateDeck(Deck deck)
        {
            var oldDeck = await _context.Decks.Where(d => d.Id == deck.Id).SingleAsync();

            _context.Entry(oldDeck).CurrentValues.SetValues(deck);

            await _context.SaveChangesAsync();

            return oldDeck;
        }
    }

}