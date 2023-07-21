namespace magix_api;

public class Faction
{
    public static readonly string EMPIRE = "Empire";
    public static readonly string REBEL = "Rebel";
    public static readonly string CRIMINAL = "Criminal";
    public static readonly string SEPARATIST = "Separatist";
    public static readonly string REPUBLIC = "Republic";

    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = default!;
}
