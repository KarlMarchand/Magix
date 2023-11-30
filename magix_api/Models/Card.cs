using System.ComponentModel.DataAnnotations.Schema;
using magix_api.utils;

namespace magix_api;

public class Card
{
    public int Id { get; set; }
    public int Cost { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    [NotMapped]
    public int? Uid { get; set; }
    [NotMapped]
    public int? BaseHP { get; set; }
    [NotMapped]
    public string? State { get; set; }
    public List<string>? Mechanics { get; set; }
    public string? Dedicated { get; set; }
    public string? CardName { get; set; }
    public string? FactionName { get; set; }
    public string? Sound { get; set; }
    public virtual ICollection<DeckCard>? DeckCard { get; set; }

    public void CompleteCard()
    {
        int index = _customCards.ContainsKey(Id) ? Id : 0;
        if (index == 0)
        {
            MissingConversions.AddNewItem<Card>(this);
        }
        CustomCard customData = _customCards[index];
        CardName = customData.CardName;
        Sound = customData.Sound;
        FactionName = customData.Faction;
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
            { 23, new CustomCard("Admiral Ackbar", Faction.REBEL,"public/assets/sounds/23.mp3")},
            { 24, new CustomCard("Asajj Ventress", Faction.SEPARATIST,"public/assets/sounds/24.mp3")},
            { 25, new CustomCard("Mace Windu", Faction.REPUBLIC,"public/assets/sounds/25.mp3")},
            { 26, new CustomCard("Chewbacca", Faction.REBEL,"public/assets/sounds/26.mp3")},
            { 27, new CustomCard("Yoda", Faction.REPUBLIC,"public/assets/sounds/27.mp3")},
            { 28, new CustomCard("Lando Calrissian", Faction.REBEL,"public/assets/sounds/28.mp3")},
            { 29, new CustomCard("Count Dooku", Faction.SEPARATIST,"public/assets/sounds/29.mp3")},
            { 30, new CustomCard("Darth Vader", Faction.EMPIRE,"public/assets/sounds/30.mp3")},
            { 31, new CustomCard("AT-ST", Faction.EMPIRE)},
            { 32, new CustomCard("AT-AT", Faction.EMPIRE)},
            { 33, new CustomCard("Director Orson Krennic", Faction.EMPIRE,"public/assets/sounds/33.mp3")},
            { 34, new CustomCard("Rebel Trooper", Faction.REBEL)},
            { 35, new CustomCard("Luke Skywalker", Faction.REBEL,"public/assets/sounds/35.mp3")},
            { 36, new CustomCard("Wookie warrior", Faction.REPUBLIC)},
            { 37, new CustomCard("Slicer", Faction.CRIMINAL)},
            { 38, new CustomCard("Jango Fett", Faction.SEPARATIST)},
            { 39, new CustomCard("Bossk", Faction.CRIMINAL,"public/assets/sounds/39.mp3")},
            { 40, new CustomCard("Bad Batch", Faction.REPUBLIC)},
            { 41, new CustomCard("Princess Leia", Faction.REBEL,"public/assets/sounds/41.mp3")},
            { 42, new CustomCard("Grand Admiral Thrawn", Faction.EMPIRE)},
            { 43, new CustomCard("General Grievous", Faction.SEPARATIST,"public/assets/sounds/43.mp3")},
            { 44, new CustomCard("Greedo", Faction.CRIMINAL)},
            { 45, new CustomCard("Black Krrsantan", Faction.CRIMINAL)},
            { 46, new CustomCard("R2-D2", Faction.REBEL)},
            { 47, new CustomCard("Black Sun Vigo", Faction.CRIMINAL,"public/assets/sounds/47.mp3")},
            { 48, new CustomCard("Occupier Assault Tank", Faction.EMPIRE)},
            { 49, new CustomCard("Admiral Trench", Faction.SEPARATIST)},
            { 50, new CustomCard("E-web Heavy Blaster", Faction.EMPIRE)},
            { 51, new CustomCard("Homing spider droid", Faction.SEPARATIST)},
            { 52, new CustomCard("Tauntaun rider", Faction.REBEL)},
            { 53, new CustomCard("Jabba the Hutt", Faction.CRIMINAL,"public/assets/sounds/53.mp3")},
            { 54, new CustomCard("Jyn Erso", Faction.REBEL,"public/assets/sounds/54.mp3")},
            { 55, new CustomCard("Imperial Commander", Faction.EMPIRE)},
            { 56, new CustomCard("AT-TE", Faction.REPUBLIC)},
            { 57, new CustomCard("Cad Bane", Faction.CRIMINAL,"public/assets/sounds/57.mp3")},
            { 58, new CustomCard("Captain Rex", Faction.REPUBLIC,"public/assets/sounds/58.mp3")},
            { 59, new CustomCard("Syndicate Henchmen", Faction.CRIMINAL)},
            { 60, new CustomCard("Death Trooper", Faction.EMPIRE)},
            { 61, new CustomCard("Droideka", Faction.SEPARATIST)},
            { 62, new CustomCard("Emperor Palpatine", Faction.EMPIRE,"public/assets/sounds/62.mp3")},
            { 63, new CustomCard("Rebel Veteran", Faction.REBEL)},
            { 64, new CustomCard("Boba Fett", Faction.CRIMINAL,"public/assets/sounds/64.mp3")},
            { 65, new CustomCard("Death Watch", Faction.CRIMINAL)},
            { 66, new CustomCard("STAP riders", Faction.SEPARATIST)},
            { 67, new CustomCard("Clone Commando", Faction.REPUBLIC)},
            { 68, new CustomCard("Dengar", Faction.CRIMINAL,"public/assets/sounds/68.mp3")},
            { 69, new CustomCard("Aayla Secura", Faction.REPUBLIC)},
            { 70, new CustomCard("Han Solo", Faction.REBEL,"public/assets/sounds/70.mp3")},
            { 71, new CustomCard("Tactical Droid", Faction.SEPARATIST)},
            { 72, new CustomCard("Inquisitor", Faction.EMPIRE)},
            { 73, new CustomCard("Super Tactical Droid", Faction.SEPARATIST)},
            { 74, new CustomCard("Armored Assault Tank", Faction.SEPARATIST)},
            { 75, new CustomCard("Dwarf Spider Droid", Faction.SEPARATIST)},
            { 76, new CustomCard("Obi-Wan Kenobi", Faction.REPUBLIC,"public/assets/sounds/76.mp3")},
            { 77, new CustomCard("Clone Trooper (Phase 2)", Faction.REPUBLIC)},
            { 78, new CustomCard("Doctor Aphra", Faction.CRIMINAL)},
            { 79, new CustomCard("Dewback Rider", Faction.EMPIRE)},
            { 80, new CustomCard("Latts Razzi", Faction.CRIMINAL)},
            { 81, new CustomCard("FD Laser Cannon", Faction.REBEL)},
            { 82, new CustomCard("Local Insurgents", Faction.REBEL)},
            { 83, new CustomCard("Assassin", Faction.CRIMINAL)},
            { 84, new CustomCard("Rebel Commander", Faction.REBEL)},
            { 85, new CustomCard("Saboteurs", Faction.REBEL)},
            { 86, new CustomCard("Darth Maul", Faction.CRIMINAL,"public/assets/sounds/86.mp3")},
            { 87, new CustomCard("Hera Syndulla", Faction.REBEL)},
            { 88, new CustomCard("BX droid commandos", Faction.SEPARATIST)},
            { 89, new CustomCard("Ahsoka Tano", Faction.REPUBLIC)},
            { 90, new CustomCard("ARC Trooper", Faction.REPUBLIC)},
            { 91, new CustomCard("Rebel AT-RT", Faction.REBEL)},
            { 92, new CustomCard("Grand Moff Tarkin", Faction.EMPIRE,"public/assets/sounds/92.mp3")},
            { 93, new CustomCard("Anakin Skywalker", Faction.REPUBLIC,"public/assets/sounds/93.mp3")},
            { 94, new CustomCard("Padme Amidala", Faction.REPUBLIC,"public/assets/sounds/94.mp3")},
            { 95, new CustomCard("IG-88", Faction.CRIMINAL)},
            { 96, new CustomCard("Rebel commandos", Faction.REBEL)},
        };
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
