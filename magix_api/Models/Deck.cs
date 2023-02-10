namespace magix_api;

public partial class Deck
{
    public int Id { get; set; }

    public int Player { get; set; }

    public string Name { get; set; } = null!;

    public int[]? Cards { get; set; }

    public string? Class { get; set; }

    public string? Talent { get; set; }

    public string? Faction { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual Player PlayerNavigation { get; set; } = null!;
}
