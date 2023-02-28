using System.Text.Json.Serialization;
using magix_api.utils;

namespace magix_api;

public class Talent : ConvertibleModel
{
    private static readonly Dictionary<string, string> _talentNameConversion = new Dictionary<string, string>{
                {"SpawnMinion", "Ambush"},
                {"ExtraCrystal", "Conscription"},
                {"ShieldProtection", "Energy_Shield"},
                {"Restorer", "Force_Healing"},
                {"LifeBoost", "Life_Ritual"},
                {"ExtraCard", "Scavenger"}
        };
    
    public int Id { get; set; }
    
    [JsonPropertyName("desc")]
    public string Description { get; set; } = default!;

    public Talent() : base(_talentNameConversion) {}

    protected override void MissConversion()
    {
        MissingConversions.AddNewItem<Talent>(this);
    }
}
