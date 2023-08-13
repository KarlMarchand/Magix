
namespace magix_api;

public class DeckCard
{
    public Guid DeckId { get; set; }
    public virtual Deck? Deck { get; set; }

    public int CardId { get; set; }
    public virtual Card? Card { get; set; }

    public int Quantity { get; set; }
}
