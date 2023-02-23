namespace magix_api;

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

public class Card
{
    private int _id;
    public int Id { 
        get => _id; 
        set{
            _id = value;
            int index = Card._customCards.ContainsKey(value) ? value : 0;
            CustomCard customData = Card._customCards[index];
            CardName = customData.CardName;
            Faction = customData.Faction;
            Sound = customData.Sound;
        }
    }
    public int Cost { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public List<string> Mechanics { get; set; } = default!;
    public string Dedicated { get; set; } = default!;
    public string? CardName { get; set; }
    public string? Faction { get; set; }
    public string? Sound { get; set; }

    private static readonly Dictionary<int, CustomCard> _customCards = new Dictionary<int, CustomCard>{
            {0, new CustomCard("Missing Card")},
            {1, new CustomCard("Minion", "empire")},
            {2, new CustomCard("IG-100 MagnaGuard", "separatist")},
            { 3, new CustomCard("Scout Trooper", "empire")},
            { 4, new CustomCard("74-Z Speeder Bike", "empire")},
            { 5, new CustomCard("Probe Droid", "empire")},
            { 6, new CustomCard("B2 super battle droid", "separatist")},
            { 7, new CustomCard("Emperor's Royal Guard", "empire")},
            { 8, new CustomCard("AT-RT scouts", "republic")},
            { 9, new CustomCard("ISB Agent", "empire")},
            { 10, new CustomCard("Pirates", "criminal")},
            { 11, new CustomCard("Persuader droid enforcer", "separatist")},
            { 12, new CustomCard("BARC speeder", "republic")},
            { 13, new CustomCard("Octuptarra Droid", "separatist")},
            { 14, new CustomCard("Clone Specialist", "republic")},
            { 15, new CustomCard("Pykes", "criminal")},
            { 16, new CustomCard("Multi-Troop Transport", "separatist")},
            { 17, new CustomCard("General Veers", "empire")},
            { 18, new CustomCard("Clone Commander", "republic")},
            { 19, new CustomCard("Ketsu Onyo", "criminal")},
            { 20, new CustomCard("Hailfire-class droid tank", "separatist")},
            { 21, new CustomCard("Saber-class fighter tank", "republic")},
            { 22, new CustomCard("Snowspeeder", "rebel")},
            { 23, new CustomCard("Admiral Ackbar", "rebel","./static/sounds/23.mp3")},
            { 24, new CustomCard("Asajj Ventress", "separatist","./static/sounds/24.mp3")},
            { 25, new CustomCard("Mace Windu", "republic","./static/sounds/25.mp3")},
            { 26, new CustomCard("Chewbacca", "rebel","./static/sounds/26.mp3")},
            { 27, new CustomCard("Yoda", "republic","./static/sounds/27.mp3")},
            { 28, new CustomCard("Lando Calrissian", "rebel","./static/sounds/28.mp3")},
            { 29, new CustomCard("Count Dooku", "separatist","./static/sounds/29.mp3")},
            { 30, new CustomCard("Darth Vader", "empire","./static/sounds/30.mp3")},
            { 31, new CustomCard("AT-ST", "empire")},
            { 32, new CustomCard("AT-AT", "empire")},
            { 33, new CustomCard("Director Orson Krennic", "empire","./static/sounds/33.mp3")},
            { 34, new CustomCard("Rebel Trooper", "rebel")},
            { 35, new CustomCard("Luke Skywalker", "rebel","./static/sounds/35.mp3")},
            { 36, new CustomCard("Wookie warrior", "republic")},
            { 37, new CustomCard("Slicer", "criminal")},
            { 38, new CustomCard("Jango Fett", "separatist")},
            { 39, new CustomCard("Bossk", "criminal","./static/sounds/39.mp3")},
            { 40, new CustomCard("Bad Batch", "republic")},
            { 41, new CustomCard("Princess Leia", "rebel","./static/sounds/41.mp3")},
            { 42, new CustomCard("Grand Admiral Thrawn", "empire")},
            { 43, new CustomCard("General Grievous", "separatist","./static/sounds/43.mp3")},
            { 44, new CustomCard("Greedo", "criminal")},
            { 45, new CustomCard("Black Krrsantan", "criminal")},
            { 46, new CustomCard("R2-D2", "rebel")},
            { 47, new CustomCard("Black Sun Vigo", "criminal","./static/sounds/47.mp3")},
            { 48, new CustomCard("Occupier Assault Tank", "empire")},
            { 49, new CustomCard("Admiral Trench", "separatist")},
            { 50, new CustomCard("E-web Heavy Blaster", "empire")},
            { 51, new CustomCard("Homing spider droid", "separatist")},
            { 52, new CustomCard("Tauntaun rider", "rebel")},
            { 53, new CustomCard("Jabba the Hutt", "criminal","./static/sounds/53.mp3")},
            { 54, new CustomCard("Jyn Erso", "rebel","./static/sounds/54.mp3")},
            { 55, new CustomCard("Imperial Commander", "empire")},
            { 56, new CustomCard("AT-TE", "republic")},
            { 57, new CustomCard("Cad Bane", "criminal","./static/sounds/57.mp3")},
            { 58, new CustomCard("Captain Rex", "republic","./static/sounds/58.mp3")},
            { 59, new CustomCard("Syndicate Henchmen", "criminal")},
            { 60, new CustomCard("Death Trooper", "empire")},
            { 61, new CustomCard("Droideka", "separatist")},
            { 62, new CustomCard("Emperor Palpatine", "empire","./static/sounds/62.mp3")},
            { 63, new CustomCard("Rebel Veteran", "rebel")},
            { 64, new CustomCard("Boba Fett", "criminal","./static/sounds/64.mp3")},
            { 65, new CustomCard("Death Watch", "criminal")},
            { 66, new CustomCard("STAP riders", "separatist")},
            { 67, new CustomCard("Clone Commando", "republic")},
            { 68, new CustomCard("Dengar", "criminal","./static/sounds/68.mp3")},
            { 69, new CustomCard("Aayla Secura", "republic")},
            { 70, new CustomCard("Han Solo", "rebel","./static/sounds/70.mp3")},
            { 71, new CustomCard("Tactical Droid", "separatist")},
            { 72, new CustomCard("Inquisitor", "empire")},
            { 73, new CustomCard("Super Tactical Droid", "separatist")},
            { 74, new CustomCard("Armored Assault Tank", "separatist")},
            { 75, new CustomCard("Dwarf Spider Droid", "separatist")},
            { 76, new CustomCard("Obi-Wan Kenobi", "republic","./static/sounds/76.mp3")},
            { 77, new CustomCard("Clone Trooper (Phase 2)", "republic")},
            { 78, new CustomCard("Doctor Aphra", "criminal")},
            { 79, new CustomCard("Dewback Rider", "empire")},
            { 80, new CustomCard("Latts Razzi", "criminal")},
            { 81, new CustomCard("FD Laser Cannon", "rebel")},
            { 82, new CustomCard("Local Insurgents", "rebel")},
            { 83, new CustomCard("Assassin", "criminal")},
            { 84, new CustomCard("Rebel Commander", "rebel")},
            { 85, new CustomCard("Saboteurs", "rebel")},
            { 86, new CustomCard("Darth Maul", "criminal","./static/sounds/86.mp3")},
            { 87, new CustomCard("Hera Syndulla", "rebel")},
            { 88, new CustomCard("BX droid commandos", "separatist")},
            { 89, new CustomCard("Ahsoka Tano", "republic")},
            { 90, new CustomCard("ARC Trooper", "republic")},
            { 91, new CustomCard("Rebel AT-RT", "rebel")},
            { 92, new CustomCard("Grand Moff Tarkin", "empire","./static/sounds/92.mp3")},
            { 93, new CustomCard("Anakin Skywalker", "republic","./static/sounds/93.mp3")},
            { 94, new CustomCard("Padme Amidala", "republic","./static/sounds/94.mp3")},
            { 95, new CustomCard("IG-88", "criminal")},
            { 96, new CustomCard("Rebel commandos", "rebel")},
        };
}
