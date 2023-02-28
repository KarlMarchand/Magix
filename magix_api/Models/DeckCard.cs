
namespace magix_api;

public class DeckCard
{
    public int DeckIdRef { get; set; }
    public int CardIdRef { get; set; }
    public int Quantity { get; set; }
    public virtual Deck Deck { get; set; } = default!;
    public virtual Card Card { get; set; } = default!;
}
