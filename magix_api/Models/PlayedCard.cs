namespace magix_api;

public partial class PlayedCard
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public virtual Player? Player { get; set; }
    public int CardId { get; set; }
    public virtual Card? Card { get; set; }
    public int TimePlayed { get; set; }
    public int Victory { get; set; }
}
