using magix_api.Data;
using magix_api.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace magix_api.Repositories
{
    public class CardRepo : ICardRepo
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MagixContext _context;

        public CardRepo(IMemoryCache memoryCache, MagixContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<List<Card>> GetAllCards()
        {
            List<Card>? result = _memoryCache.Get<List<Card>>("cards");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Card>> response = await GameServerAPI.CallApi<List<Card>>("cards");
                    if (response.IsValid && response.Content != null)
                    {
                        result = response.Content;
                        await UpdateDatabaseCardsDataAsync(result);
                        var updatedList = _memoryCache.Set("cards", result, TimeSpan.FromDays(1));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Card>();
        }

        public async Task<Card?> GetCardById(int id)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Card>> GetCardsByIds(List<int> cardIds)
        {
            var distinctCards = await _context.Cards.Where(card => cardIds.Contains(card.Id)).ToListAsync();
            List<Card> finalCardList = new List<Card>();
            foreach (var id in cardIds)
            {
                var card = distinctCards.FirstOrDefault(c => c.Id == id);
                if (card != null)
                {
                    card.CompleteCard();
                    finalCardList.Add(card);
                }
                else
                {
                    throw new Exception("An error occured while retrieving one of the Card");
                }
            }
            return finalCardList;
        }

        private async Task UpdateDatabaseCardsDataAsync(List<Card> cards)
        {
            var cardIds = cards.Select(c => c.Id).ToList();
            var existingCards = await _context.Cards.Where(c => cardIds.Contains(c.Id)).ToListAsync();
            var existingCardsDict = existingCards.ToDictionary(c => c.Id, c => c);

            var properties = typeof(Card).GetProperties().Where(p => p.Name != "Id" && p.CanWrite).ToList();

            foreach (var apiCard in cards)
            {
                if (existingCardsDict.TryGetValue(apiCard.Id, out var card))
                {
                    foreach (var property in properties)
                    {
                        var newValue = property.GetValue(apiCard);
                        property.SetValue(card, newValue);
                    }
                }
                else
                {
                    _context.Cards.Add(apiCard);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
