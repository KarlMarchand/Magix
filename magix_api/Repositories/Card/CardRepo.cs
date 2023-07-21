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
            List<Card>? result = null;
            // result = _memoryCache.Get<List<Card>>("cards");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Card>> response = await GameServerAPI.CallApi<List<Card>>("cards");
                    if (response.IsValid && response.Content != null)
                    {
                        List<Card> apiCards = response.Content;
                        foreach (var apiCard in apiCards)
                        {
                            apiCard.CompleteCard();
                            var card = _context.Cards.SingleOrDefault(c => c.Id == apiCard.Id);
                            if (card is not null)
                            {
                                foreach (var property in card.GetType().GetProperties())
                                {
                                    if (property.Name != "Id")
                                        property.SetValue(card, property.GetValue(apiCard));
                                }
                            }
                            else
                            {
                                _context.Cards.Add(apiCard);
                            }
                        }
                        await _context.SaveChangesAsync();
                        result = _context.Cards.AsNoTracking().ToList();
                        // var updatedList = _memoryCache.Set("cards", result, TimeSpan.FromDays(1));
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
    }
}
