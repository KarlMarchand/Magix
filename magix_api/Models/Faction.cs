namespace magix_api;

public class Faction
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Faction(string name, string description)
    {
        Description = description;
        Name = name;
    }
}
