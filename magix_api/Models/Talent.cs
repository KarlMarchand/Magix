using magix_api.Dtos.Talent;

namespace magix_api.Models
{
    public class Talent
    {
        public string Name { get; }
        public string Description { get; }

        public Talent(ServerTalentDto gameServerVersion)
        {
            Name = ConvertName(gameServerVersion.Name);
            Description = gameServerVersion.Desc;
        }

        private static string ConvertName(string originalData)
        {
            return _talentConversion.TryGetValue(originalData, out string? convertedData) ? convertedData : originalData;
        }

        private static readonly Dictionary<string, string> _talentConversion = new Dictionary<string, string>{
                {"SpawnMinion", "Ambush"},
                {"ExtraCrystal", "Conscription"},
                {"ShieldProtection", "Energy_Shield"},
                {"Restorer", "Force_Healing"},
                {"LifeBoost", "Life_Ritual"},
                {"ExtraCard", "Scavenger"}
        };
    }
}