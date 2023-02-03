using System;
using System.Collections.Generic;

namespace magix_api;

public partial class Player
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Class { get; set; }

    public int? Trophies { get; set; }

    public int? BestTrophyScore { get; set; }

    public string? Faction { get; set; }

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual ICollection<PlayedCard> PlayedCards { get; } = new List<PlayedCard>();
}
