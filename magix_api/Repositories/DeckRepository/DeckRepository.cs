using Microsoft.Extensions.Caching.Memory;
using magix_api.Dtos.CardDto;
using magix_api.utils;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;


namespace magix_api.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        private readonly IMemoryCache _memoryCache;

        public DeckRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<Card>> GetAllCards()
        {
            List<Card>? result = null;
            //result = _memoryCache.Get<List<Card>>("cards");
            if (result is null)
            {
                try
                {
                    List<ServerCardDto>? list = await GameServerAPI.CallApi<List<ServerCardDto>>("cards");
                    if (list != null)
                    {
                        result = list.Select(x => new Card(x)).ToList();
                        // var input = _memoryCache.Set("cards", result, TimeSpan.FromDays(1));
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
            return new List<Faction>(){
                new Faction("Empire","Use the imperial war machine to crush your ennemy with expensive but powerful units."),
                new Faction("Rebel","Rebellion are built on hope... and stealth. Experts in deception and ambush to strike when the enemy is off-guard."),
                new Faction("Republic","Get behind the legendary Jedi and their mystical powers and beat those clankers."),
                new Faction("Separatist","Cheap troops and sheer numbers will win this war. They can't beat us all, we are legion."),
                new Faction("Criminal","When you need something done right, you hire the best of the best. With specialised troops and shady characters, the real power lies in the shadow.")
            };
        }

        public async Task<List<Hero>> GetAllHeroes()
        {
            List<Hero>? result = null;
            // result = _memoryCache.Get<List<Hero>>("heroes");
            if (result is null)
            {
                try
                {
                    List<ServerHeroDto>? list = await GameServerAPI.CallApi<List<ServerHeroDto>>("heroes");
                    if (list != null)
                    {
                        result = list.Select(x =>
                        {
                            Hero hero = new Hero(x);
                            hero.ToFrontend();
                            return hero;
                        }).ToList();
                        // var input = _memoryCache.Set("heroes", result, TimeSpan.FromDays(1));
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
                    List<ServerTalentDto>? list = await GameServerAPI.CallApi<List<ServerTalentDto>>("talents");
                    if (list != null)
                    {
                        result = list.Select(x =>
                        {
                            Talent talent = new Talent(x);
                            talent.ToFrontend();
                            return talent;
                        }).ToList();
                        // var input = _memoryCache.Set("talents", result, TimeSpan.FromDays(1));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Talent>();
        }
    }
}