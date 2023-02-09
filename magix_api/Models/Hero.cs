using magix_api.Dtos.Hero;

namespace magix_api.Models
{
    public class Hero
    {
        public string Name { get; }
        public string Power { get; }

        public Hero(ServerHeroDto gameServerVersion)
        {
            Name = ConvertName(gameServerVersion.Name);
            Power = gameServerVersion.Power;
        }

        private static string ConvertName(string originalData)
        {
            return _nameConversion.TryGetValue(originalData, out string? convertedData) ? convertedData : originalData;
        }

        private static readonly Dictionary<string, string> _nameConversion = new Dictionary<string, string>{
                {"DemonHunter", "Bounty_Hunter"},
                {"Hunter", "Ace_Pilot"},
                {"Priest", "Diplomat"},
                {"Druid", "Scout"},
                {"Paladin", "Guardian"},
                {"Warlock", "Mystic"},
                {"Shaman", "Night_Sister"},
                {"Rogue", "Smuggler"},
                {"Warrior", "Soldier"},
                {"Mage", "Slicer"},
        };
    }
}