using magix_api.Custom_Exceptions;
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

        public async Task<List<Deck>> GetAllDecks(int playerId)
        {
            List<Deck> decks = await _context.Decks
                .Include(d => d.DeckCards)
                    .ThenInclude(dc => dc.Card)
                .Where(d => d.PlayerId == playerId)
                .ToListAsync();

            foreach (var deck in decks)
            {
                deck.Cards = deck.DeckCards
                    .SelectMany(dc => Enumerable.Repeat(dc.Card!, dc.Quantity))
                    .ToList();
            }

            return decks;
        }

        public async Task<Deck?> GetDeck(Guid? deckId = null, int? playerId = null)
        {
            Deck? deck = null;
            var query = _context.Decks
                .Include(d => d.Faction)
                .Include(d => d.Hero)
                .Include(d => d.Talent)
                .Include(p => p.DeckCards)
                    .ThenInclude(p => p.Card);

            if (deckId != null)
            {
                deck = await query.FirstOrDefaultAsync(d => d.Id == deckId);
            }
            else if (playerId != null)
            {
                // This means we're looking for the active deck.
                deck = await query.FirstOrDefaultAsync(d => d.PlayerId == playerId && d.Active);
            }

            if (deck != null)
            {
                deck.Cards = deck.DeckCards
                    .SelectMany(dc => Enumerable.Repeat(dc.Card!, dc.Quantity))
                    .ToList();
            }

            return deck;
        }

        public async Task<Deck> UpdateDeck(Deck deck)
        {
            var oldDeck = _context.Decks.Find(deck.Id);

            if (oldDeck != null)
            {
                // Make sure the user is authorized to update this deck.
                if (oldDeck.PlayerId != deck.PlayerId)
                {
                    throw new ResourceUnauthorizedException();
                }
                _context.Entry(oldDeck).CurrentValues.SetValues(deck);
                await _context.SaveChangesAsync();
            }

            return oldDeck ?? deck;
        }

        public async Task<bool> DeleteDeck(Guid deckId, int requesterId)
        {
            return (await _context.Decks.Where(d => d.Id == deckId && d.PlayerId == requesterId).ExecuteDeleteAsync()) > 0;
        }

        // Deactivate all active decks and activate only this deck
        public async Task UpdateActiveDeck(Deck deck)
        {
            deck.Active = true;

            // Find all the active decks for the player except the provided deck.
            var activeDeckIds = await _context.Decks
                .AsNoTracking()
                .Where(d => d.PlayerId == deck.PlayerId && d.Active && d.Id != deck.Id)
                .Select(d => d.Id)
                .ToListAsync();

            // If there are active decks, deactivate them in a single update query.
            if (activeDeckIds.Count > 0)
            {
                var parameterizedIds = string.Join(", ", activeDeckIds.Select((id, index) => $"@p{index}"));
                var sql = $"UPDATE Decks SET Active = 0 WHERE Id IN ({parameterizedIds})";

                await _context.Database.ExecuteSqlRawAsync(sql, activeDeckIds.Cast<object>().ToArray());
            }

            // If the deck is in the context (i.e., it's not new), mark it as modified.
            if (_context.Entry(deck).State != EntityState.Detached)
            {
                _context.Entry(deck).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}