namespace magix_api;

public partial class Game
{
    public int Id { get; set; }

    public int Player { get; set; }

    public int Deck { get; set; }

    public string Opponent { get; set; } = default!;

    public DateOnly Date { get; set; }

    public bool Won { get; set; }

    public virtual Deck DeckNavigation { get; set; } = default!;

    public virtual Player PlayerNavigation { get; set; } = default!;
}
