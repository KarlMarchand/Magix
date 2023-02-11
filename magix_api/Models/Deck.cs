namespace magix_api;

public partial class Deck
{
    public int Id { get; set; }

    public int Player { get; set; }

    public string Name { get; set; } = null!;

    public int[] Cards { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string Talent { get; set; } = null!;

    public string Faction { get; set; } = null!;

    public bool Active { get; set; } = true;

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual Player PlayerNavigation { get; set; } = null!;
}
