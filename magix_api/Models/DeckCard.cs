
namespace magix_api;

public class DeckCard
{
    public int DeckIdRef { get; set; }
    public virtual Deck Deck { get; set; } = default!;

    public int CardIdRef { get; set; }
    public virtual Card Card { get; set; } = default!;

    public int Quantity { get; set; }
}
