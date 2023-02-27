using System.ComponentModel.DataAnnotations.Schema;

namespace magix_api;

public partial class Player
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;

    [NotMapped]
    public string? ClassName { get; set; }

    public int WinCount { get; set; }

    public int LossCount { get; set; }

    public DateTime LastLogin { get; set; } = DateTime.Now;

    public string? WelcomeText { get; set; }

    public int Trophies { get; set; }

    public int BestTrophyScore { get; set; }

    public virtual ICollection<Deck> Decks { get; } = new List<Deck>();

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual ICollection<PlayedCard> PlayedCards { get; } = new List<PlayedCard>();

    [NotMapped]
    public string? Key {get; set;}
    
    [NotMapped]
    public PlayerStat? Stats { get; set; }
}
