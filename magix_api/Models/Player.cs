using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace magix_api;

[Index(nameof(Username), IsUnique = true)]
public partial class Player
{
    public int Id { get; set; }

    [Required]
    public required string Username { get; set; }

    [NotMapped]
    public string? ClassName { get; set; }

    public int WinCount { get; set; }

    public int LossCount { get; set; }

    public DateTime LastLogin { get; set; } = DateTime.Now;

    public string? WelcomeText { get; set; }

    public int Trophies { get; set; }

    public int BestTrophyScore { get; set; }

    public virtual IList<Deck> Decks { get; set; }

    public virtual IList<Game> Games { get; set; }

    public virtual IList<PlayedCard> PlayedCards { get; set; }

    [NotMapped]
    public string? Key { get; set; }

    [NotMapped]
    public PlayerStat? Stats { get; set; }
}
