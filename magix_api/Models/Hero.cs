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

    public string Power { get; set; } = default!;

    public Hero() : base(_heroNameConversion) {}
}
