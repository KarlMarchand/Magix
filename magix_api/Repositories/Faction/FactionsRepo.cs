namespace magix_api.Repositories
{
    public class FactionsRepo : IFactionsRepo
    {
        private Dictionary <string, Faction> _customFactions;
        
        public FactionsRepo()
        {
            var empireFaction = new Faction{Name=Faction.EMPIRE,Description="Use the imperial war machine to crush your ennemy with expensive but powerful units."};
            var rebelFaction = new Faction{Name=Faction.REBEL,Description="Rebellion are built on hope... and stealth. Experts in deception and ambush to strike when the enemy is off-guard."};
            var republicFaction = new Faction{Name=Faction.REPUBLIC,Description="Get behind the legendary Jedi and their mystical powers and beat those clankers."};
            var separatistFaction = new Faction{Name=Faction.SEPARATIST,Description="Cheap troops and sheer numbers will win this war. They can't beat us all, we are legion."};
            var criminalFaction = new Faction{Name=Faction.CRIMINAL,Description="When you need something done right, you hire the best of the best. With specialised troops and shady characters, the real power lies in the shadow."};
            _customFactions = BuildFactionsDictionnary(new List<Faction>{empireFaction, rebelFaction, republicFaction, separatistFaction, criminalFaction});
        }

        private Dictionary <string, Faction> BuildFactionsDictionnary(List<Faction> factions)
        {
            var dictionary = new Dictionary <string, Faction>();
            factions.ForEach(f => {
                dictionary.Add(f.Name, f);
            });
            return dictionary;
        }

        public Faction GetFaction (string factionName)
        {
            return _customFactions[factionName];
        }

        public List<Faction> GetAllFactions ()
        {
            return _customFactions.Values.ToList();
        }
    }
}