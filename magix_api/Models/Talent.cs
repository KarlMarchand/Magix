using magix_api.Dtos.TalentDto;

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
    public string Description { get; set; }

    public Talent(ServerTalentDto gameServerVersion) : base(_talentNameConversion)
    {
        Name = gameServerVersion.name;
        Description = gameServerVersion.desc;
    }

    public Talent(GetTalentDto frontendVersion) : base(_talentNameConversion)
    {
        Name = frontendVersion.Name;
        Description = frontendVersion.Description;
    }
}
