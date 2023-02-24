namespace magix_api;

public partial class PlayedCard
{
    public int Id { get; set; }

    public Player Player { get; set; } = default!;

    public Card CardId { get; set; } = default!;

    public int TimePlayed { get; set; }

    public int Victory { get; set; }
}
