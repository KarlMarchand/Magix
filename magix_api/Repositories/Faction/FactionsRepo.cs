using magix_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace magix_api.Repositories
{
    public class FactionsRepo : IFactionsRepo
    {
        private readonly Dictionary<string, Faction> _customFactions;
        private readonly IMemoryCache _memoryCache;
        private readonly MagixContext _context;

        public FactionsRepo(IMemoryCache memoryCache, MagixContext context)
        {
            _context = context;
            _memoryCache = memoryCache;
            var empireFaction = new Faction { Id = 1, Name = Faction.EMPIRE, Description = "Use the imperial war machine to crush your ennemy with expensive but powerful units." };
            var rebelFaction = new Faction { Id = 2, Name = Faction.REBEL, Description = "Rebellion are built on hope... and stealth. Experts in deception and ambush to strike when the enemy is off-guard." };
            var republicFaction = new Faction { Id = 3, Name = Faction.REPUBLIC, Description = "Get behind the legendary Jedi and their mystical powers and beat those clankers." };
            var separatistFaction = new Faction { Id = 4, Name = Faction.SEPARATIST, Description = "Cheap troops and sheer numbers will win this war. They can't beat us all, we are legion." };
            var criminalFaction = new Faction { Id = 5, Name = Faction.CRIMINAL, Description = "When you need something done right, you hire the best of the best. With specialised troops and shady characters, the real power lies in the shadow." };
            _customFactions = BuildFactionsDictionnary(new List<Faction> { empireFaction, rebelFaction, republicFaction, separatistFaction, criminalFaction });
        }

        private Dictionary<string, Faction> BuildFactionsDictionnary(List<Faction> factions)
        {
            var dictionary = new Dictionary<string, Faction>();
            factions.ForEach(f =>
            {
                dictionary.Add(f.Name, f);
            });
            return dictionary;
        }

        public Faction GetFactionByName(string factionName)
        {
            return _customFactions[factionName];
        }
        public async Task<Faction?> GetFactionById(int id)
        {
            return await _context.Factions.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Faction>> GetAllFactions()
        {
            List<Faction>? result = _memoryCache.Get<List<Faction>>("factions");
            if (result is null)
            {
                try
                {
                    result = _customFactions.Values.ToList();
                    if (!_context.Factions.Any())
                    {
                        await AddFactionsToDatabase(result);
                    }
                    if (result != null && result.Count > 0)
                    {
                        _memoryCache.Set("factions", result, TimeSpan.FromDays(1));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result ?? new List<Faction>();
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