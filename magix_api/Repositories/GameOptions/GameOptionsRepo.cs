using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using magix_api.utils;
using magix_api.Data;

namespace magix_api.Repositories
{
    public class GameOptionsRepo : IGameOptionsRepo
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MagixContext _context;
        private readonly IFactionsRepo _factionRepo;

        public GameOptionsRepo(IMemoryCache memoryCache, MagixContext context, IFactionsRepo factionRepo)
        {
            _memoryCache = memoryCache;
            _context = context;
            _factionRepo = factionRepo;
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

        public async Task<List<Faction>> GetAllFactions()
        {
            List<Faction>? result = null;
            // result = _memoryCache.Get<List<Faction>>("factions");
            if (result is null)
            {
                try
                {
                    result = _factionRepo.GetAllFactions();
                    if (!_context.Factions.Any())
                    {
                        await AddFactionsToDatabase(result);
                    }
                    if (result != null && result.Count > 0)
                    {
                        // _memoryCache.Set("factions", result, TimeSpan.FromDays(1));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Faction>();
        }

        public async Task<List<Hero>> GetAllHeroes()
        {
            List<Hero>? result = null;
            // result = _memoryCache.Get<List<Hero>>("heroes");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Hero>> response = await GameServerAPI.CallApi<List<Hero>>("heroes");
                    if (response.IsValid && response.Content != null)
                    {
                        result = response.Content.Select(hero =>
                        {
                            hero.ToFrontend();
                            var dbHero = _context.Heroes.SingleOrDefault(h => h.Name == hero.Name);
                            if (dbHero is not null)
                            {
                                dbHero.Power = hero.Power;
                            }
                            else
                            {
                                _context.Heroes.Add(hero);
                            }
                            return hero;
                        }).ToList();
                        // var input = _memoryCache.Set("heroes", result, TimeSpan.FromDays(1));
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Hero>();
        }

        public async Task<List<Talent>> GetAllTalents()
        {
            List<Talent>? result = null;
            // result = _memoryCache.Get<List<Talent>>("talents");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Talent>> response = await GameServerAPI.CallApi<List<Talent>>("talents");
                    if (response.IsValid && response.Content != null)
                    {
                        result = response.Content.Select(talent =>
                        {
                            talent.ToFrontend();
                            var dbTalent = _context.Talents.SingleOrDefault(t => t.Name == talent.Name);
                            if (dbTalent is not null)
                            {
                                dbTalent.Description = talent.Description;
                            }
                            else
                            {
                                _context.Talents.Add(talent);
                            }
                            return talent;
                        }).ToList();
                        // var input = _memoryCache.Set("talents", result, TimeSpan.FromDays(1));
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Talent>();
        }

        private async Task AddFactionsToDatabase(List<Faction> factions)
        {
            foreach (var faction in factions)
            {
                var dbFaction = _context.Factions.SingleOrDefault(f => f.Name == faction.Name);
                if (dbFaction is not null)
                {
                    _context.Entry(dbFaction).CurrentValues.SetValues(faction);
                }
                else
                {
                    _context.Factions.Add(faction);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}