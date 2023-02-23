namespace magix_api;

public partial class Deck
{
    public int Id { get; set; }

    public int Player { get; set; }

    public string Name { get; set; } = default!;

    public int[] Cards { get; set; } = default!;

    public string Class { get; set; } = default!;

    public string Talent { get; set; } = default!;

    public string Faction { get; set; } = default!;

    public bool Active { get; set; } = true;

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual Player PlayerNavigation { get; set; } = default!;
}
