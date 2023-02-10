using magix_api.Dtos.Hero;
using magix_api.Class;

namespace magix_api;

public class Hero : ConvertibleModel
{
    private static readonly Dictionary<string, string> _heroNameConversion = new Dictionary<string, string>{
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

    public string Power { get; set; }

    public Hero(ServerHeroDto gameServerVersion) : base(_heroNameConversion)
    {
        Name = gameServerVersion.Name;
        Power = gameServerVersion.Power;
    }

    public Hero(GetHeroDto frontendVersion) : base(_heroNameConversion)
    {
        Name = frontendVersion.Name;
        Power = frontendVersion.Power;
    }
}
