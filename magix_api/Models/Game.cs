namespace magix_api;

public partial class Game
{
    public int Id { get; set; }

    public int DeckId { get; set; }
    public virtual Deck? Deck { get; set; }

    public string? Opponent { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public bool Won { get; set; }

    public int PlayerId { get; set; }
    public virtual Player? Player { get; set; }
}
