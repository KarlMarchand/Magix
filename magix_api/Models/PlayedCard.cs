namespace magix_api;

public partial class PlayedCard
{
    public long Id { get; set; }

    public int Player { get; set; }

    public int CardId { get; set; }

    public int TimePlayed { get; set; }

    public int Victory { get; set; }

    public virtual Player PlayerNavigation { get; set; } = null!;
}
