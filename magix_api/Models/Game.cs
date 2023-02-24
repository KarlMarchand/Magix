namespace magix_api;

public partial class Game
{
    public int Id { get; set; }

    public Deck Deck { get; set; } = default!;

    public string Opponent { get; set; } = default!;

    public DateTime Date { get; set; } = DateTime.Now;

    public bool Won { get; set; }

    public Player player { get; set; } = default!;
}
