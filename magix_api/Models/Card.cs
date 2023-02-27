namespace magix_api;

public class Card
{
    public int Id {get; set;}
    public int Cost { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int? Uid { get; set; }
    public int? BaseHP { get; set; }
    public string? State { get; set; }
    public List<string>? Mechanics { get; set; }
    public string? Dedicated { get; set; }
    public string CardName { get; set; } = default!;
    public Faction? Faction { get; set; }
    public string? Sound { get; set; }
}

public class CustomCard
{
    public string CardName { get; set; }
    public string? Faction { get; set; }
    public string? Sound { get; set; }

    public CustomCard(string cardName, string? faction = null, string? sound = null)
    {
        CardName = cardName;
        Faction = faction;
        Sound = sound;
    }
}
