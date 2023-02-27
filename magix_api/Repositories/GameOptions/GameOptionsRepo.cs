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
        private Dictionary <string, Faction> _customFactions;

        public GameOptionsRepo(IMemoryCache memoryCache, MagixContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
            _customFactions = new();
            GetAllFactions().RunSynchronously();
        }

        public async Task<List<Card>> GetAllCards()
        {
            List<Card>? result = null;
            //result = _memoryCache.Get<List<Card>>("cards");
            if (result is null)
            {
                try
                {
                    ServerResponse<List<Card>> response = await GameServerAPI.CallApi<List<Card>>("cards");
                    if (response.IsValid && response.Content != null)
                    {
                        List<Card> apiCards = response.Content;
                        foreach (var card in apiCards)
                        {
                            CompleteCard(in card);
                        }
                        // var input = _memoryCache.Set("cards", apiCards, TimeSpan.FromDays(1));
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
            result = _memoryCache.Get<List<Faction>>("factions");
            if (result is null)
            {
                try
                {
                    if (!_context.Factions.Any()) {
                        await AddFactionsToDatabase();
                    }
                    result = _context.Factions
                        .AsNoTracking()
                        .ToList();
                    if (result != null && result.Count>0)
                    {
                        _memoryCache.Set("factions", result, TimeSpan.FromDays(1));
                        BuildFactionsDictionnary(result);
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
                    ServerResponse<List<Talent>> response = await GameServerAPI.CallApi<List<Talent>>("talents");
                    if (response.IsValid && response.Content != null)
                    {
                        result = response.Content.Select(talent =>
                        {
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

        private async Task AddFactionsToDatabase()
        {
            var empireFaction = new Faction{Name=Faction.EMPIRE,Description="Use the imperial war machine to crush your ennemy with expensive but powerful units."};
            var rebelFaction = new Faction{Name=Faction.REBEL,Description="Rebellion are built on hope... and stealth. Experts in deception and ambush to strike when the enemy is off-guard."};
            var republicFaction = new Faction{Name=Faction.REPUBLIC,Description="Get behind the legendary Jedi and their mystical powers and beat those clankers."};
            var separatistFaction = new Faction{Name=Faction.SEPARATIST,Description="Cheap troops and sheer numbers will win this war. They can't beat us all, we are legion."};
            var criminalFaction = new Faction{Name=Faction.CRIMINAL,Description="When you need something done right, you hire the best of the best. With specialised troops and shady characters, the real power lies in the shadow."};
            await _context.Factions.AddRangeAsync(new Faction[]{empireFaction, rebelFaction, republicFaction, separatistFaction, criminalFaction});
        }

        private Dictionary <string, Faction> BuildFactionsDictionnary(List<Faction> factions)
        {
            var dictionary = new Dictionary <string, Faction>();
            factions.ForEach(f => {
                dictionary.Add(f.Name, f);
            });
            return dictionary;
        }

        public void CompleteCard(in Card incompleteCard)
        {
            int index;
            if (_customCards.ContainsKey(incompleteCard.Id)){
                index = incompleteCard.Id;
            } else {
                index = 0;
                MissingConversions.AddNewItem<Card>(incompleteCard);
            }
            CustomCard customData = _customCards[index];
            incompleteCard.CardName = customData.CardName;
            incompleteCard.Sound = customData.Sound;
            if (customData.Faction != null)
                incompleteCard.Faction = _customFactions[customData.Faction];
        }

        private static readonly Dictionary<int, CustomCard> _customCards = new Dictionary<int, CustomCard>{
            { 0, new CustomCard("Missing Card")},
            { 1, new CustomCard("Minion")},
            { 2, new CustomCard("IG-100 MagnaGuard", Faction.SEPARATIST)},
            { 3, new CustomCard("Scout Trooper", Faction.EMPIRE)},
            { 4, new CustomCard("74-Z Speeder Bike", Faction.EMPIRE)},
            { 5, new CustomCard("Probe Droid", Faction.EMPIRE)},
            { 6, new CustomCard("B2 super battle droid", Faction.SEPARATIST)},
            { 7, new CustomCard("Emperor's Royal Guard", Faction.EMPIRE)},
            { 8, new CustomCard("AT-RT scouts", Faction.REPUBLIC)},
            { 9, new CustomCard("ISB Agent", Faction.EMPIRE)},
            { 10, new CustomCard("Pirates", Faction.CRIMINAL)},
            { 11, new CustomCard("Persuader droid enforcer", Faction.SEPARATIST)},
            { 12, new CustomCard("BARC speeder", Faction.REPUBLIC)},
            { 13, new CustomCard("Octuptarra Droid", Faction.SEPARATIST)},
            { 14, new CustomCard("Clone Specialist", Faction.REPUBLIC)},
            { 15, new CustomCard("Pykes", Faction.CRIMINAL)},
            { 16, new CustomCard("Multi-Troop Transport", Faction.SEPARATIST)},
            { 17, new CustomCard("General Veers", Faction.EMPIRE)},
            { 18, new CustomCard("Clone Commander", Faction.REPUBLIC)},
            { 19, new CustomCard("Ketsu Onyo", Faction.CRIMINAL)},
            { 20, new CustomCard("Hailfire-class droid tank", Faction.SEPARATIST)},
            { 21, new CustomCard("Saber-class fighter tank", Faction.REPUBLIC)},
            { 22, new CustomCard("Snowspeeder", Faction.REBEL)},
            { 23, new CustomCard("Admiral Ackbar", Faction.REBEL,"./static/sounds/23.mp3")},
            { 24, new CustomCard("Asajj Ventress", Faction.SEPARATIST,"./static/sounds/24.mp3")},
            { 25, new CustomCard("Mace Windu", Faction.REPUBLIC,"./static/sounds/25.mp3")},
            { 26, new CustomCard("Chewbacca", Faction.REBEL,"./static/sounds/26.mp3")},
            { 27, new CustomCard("Yoda", Faction.REPUBLIC,"./static/sounds/27.mp3")},
            { 28, new CustomCard("Lando Calrissian", Faction.REBEL,"./static/sounds/28.mp3")},
            { 29, new CustomCard("Count Dooku", Faction.SEPARATIST,"./static/sounds/29.mp3")},
            { 30, new CustomCard("Darth Vader", Faction.EMPIRE,"./static/sounds/30.mp3")},
            { 31, new CustomCard("AT-ST", Faction.EMPIRE)},
            { 32, new CustomCard("AT-AT", Faction.EMPIRE)},
            { 33, new CustomCard("Director Orson Krennic", Faction.EMPIRE,"./static/sounds/33.mp3")},
            { 34, new CustomCard("Rebel Trooper", Faction.REBEL)},
            { 35, new CustomCard("Luke Skywalker", Faction.REBEL,"./static/sounds/35.mp3")},
            { 36, new CustomCard("Wookie warrior", Faction.REPUBLIC)},
            { 37, new CustomCard("Slicer", Faction.CRIMINAL)},
            { 38, new CustomCard("Jango Fett", Faction.SEPARATIST)},
            { 39, new CustomCard("Bossk", Faction.CRIMINAL,"./static/sounds/39.mp3")},
            { 40, new CustomCard("Bad Batch", Faction.REPUBLIC)},
            { 41, new CustomCard("Princess Leia", Faction.REBEL,"./static/sounds/41.mp3")},
            { 42, new CustomCard("Grand Admiral Thrawn", Faction.EMPIRE)},
            { 43, new CustomCard("General Grievous", Faction.SEPARATIST,"./static/sounds/43.mp3")},
            { 44, new CustomCard("Greedo", Faction.CRIMINAL)},
            { 45, new CustomCard("Black Krrsantan", Faction.CRIMINAL)},
            { 46, new CustomCard("R2-D2", Faction.REBEL)},
            { 47, new CustomCard("Black Sun Vigo", Faction.CRIMINAL,"./static/sounds/47.mp3")},
            { 48, new CustomCard("Occupier Assault Tank", Faction.EMPIRE)},
            { 49, new CustomCard("Admiral Trench", Faction.SEPARATIST)},
            { 50, new CustomCard("E-web Heavy Blaster", Faction.EMPIRE)},
            { 51, new CustomCard("Homing spider droid", Faction.SEPARATIST)},
            { 52, new CustomCard("Tauntaun rider", Faction.REBEL)},
            { 53, new CustomCard("Jabba the Hutt", Faction.CRIMINAL,"./static/sounds/53.mp3")},
            { 54, new CustomCard("Jyn Erso", Faction.REBEL,"./static/sounds/54.mp3")},
            { 55, new CustomCard("Imperial Commander", Faction.EMPIRE)},
            { 56, new CustomCard("AT-TE", Faction.REPUBLIC)},
            { 57, new CustomCard("Cad Bane", Faction.CRIMINAL,"./static/sounds/57.mp3")},
            { 58, new CustomCard("Captain Rex", Faction.REPUBLIC,"./static/sounds/58.mp3")},
            { 59, new CustomCard("Syndicate Henchmen", Faction.CRIMINAL)},
            { 60, new CustomCard("Death Trooper", Faction.EMPIRE)},
            { 61, new CustomCard("Droideka", Faction.SEPARATIST)},
            { 62, new CustomCard("Emperor Palpatine", Faction.EMPIRE,"./static/sounds/62.mp3")},
            { 63, new CustomCard("Rebel Veteran", Faction.REBEL)},
            { 64, new CustomCard("Boba Fett", Faction.CRIMINAL,"./static/sounds/64.mp3")},
            { 65, new CustomCard("Death Watch", Faction.CRIMINAL)},
            { 66, new CustomCard("STAP riders", Faction.SEPARATIST)},
            { 67, new CustomCard("Clone Commando", Faction.REPUBLIC)},
            { 68, new CustomCard("Dengar", Faction.CRIMINAL,"./static/sounds/68.mp3")},
            { 69, new CustomCard("Aayla Secura", Faction.REPUBLIC)},
            { 70, new CustomCard("Han Solo", Faction.REBEL,"./static/sounds/70.mp3")},
            { 71, new CustomCard("Tactical Droid", Faction.SEPARATIST)},
            { 72, new CustomCard("Inquisitor", Faction.EMPIRE)},
            { 73, new CustomCard("Super Tactical Droid", Faction.SEPARATIST)},
            { 74, new CustomCard("Armored Assault Tank", Faction.SEPARATIST)},
            { 75, new CustomCard("Dwarf Spider Droid", Faction.SEPARATIST)},
            { 76, new CustomCard("Obi-Wan Kenobi", Faction.REPUBLIC,"./static/sounds/76.mp3")},
            { 77, new CustomCard("Clone Trooper (Phase 2)", Faction.REPUBLIC)},
            { 78, new CustomCard("Doctor Aphra", Faction.CRIMINAL)},
            { 79, new CustomCard("Dewback Rider", Faction.EMPIRE)},
            { 80, new CustomCard("Latts Razzi", Faction.CRIMINAL)},
            { 81, new CustomCard("FD Laser Cannon", Faction.REBEL)},
            { 82, new CustomCard("Local Insurgents", Faction.REBEL)},
            { 83, new CustomCard("Assassin", Faction.CRIMINAL)},
            { 84, new CustomCard("Rebel Commander", Faction.REBEL)},
            { 85, new CustomCard("Saboteurs", Faction.REBEL)},
            { 86, new CustomCard("Darth Maul", Faction.CRIMINAL,"./static/sounds/86.mp3")},
            { 87, new CustomCard("Hera Syndulla", Faction.REBEL)},
            { 88, new CustomCard("BX droid commandos", Faction.SEPARATIST)},
            { 89, new CustomCard("Ahsoka Tano", Faction.REPUBLIC)},
            { 90, new CustomCard("ARC Trooper", Faction.REPUBLIC)},
            { 91, new CustomCard("Rebel AT-RT", Faction.REBEL)},
            { 92, new CustomCard("Grand Moff Tarkin", Faction.EMPIRE,"./static/sounds/92.mp3")},
            { 93, new CustomCard("Anakin Skywalker", Faction.REPUBLIC,"./static/sounds/93.mp3")},
            { 94, new CustomCard("Padme Amidala", Faction.REPUBLIC,"./static/sounds/94.mp3")},
            { 95, new CustomCard("IG-88", Faction.CRIMINAL)},
            { 96, new CustomCard("Rebel commandos", Faction.REBEL)},
        };
    }
}