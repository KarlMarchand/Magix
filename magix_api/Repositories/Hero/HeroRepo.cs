using magix_api.Data;
using magix_api.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace magix_api.Repositories
{
    public class HeroRepo : IHeroRepo
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MagixContext _context;

        public HeroRepo(IMemoryCache memoryCache, MagixContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
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

        public async Task<Hero?> GetHeroById(int id)
        {
            return await _context.Heroes.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hero?> GetHeroByName(string name)
        {
            return await _context.Heroes.FirstOrDefaultAsync(h => h.Name == name);
        }
    }
}
