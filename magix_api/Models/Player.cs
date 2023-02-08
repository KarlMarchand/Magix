using System;
using System.Collections.Generic;

namespace magix_api;

public partial class Player
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int? Trophies { get; set; }

    public int? BestTrophyScore { get; set; }

    public virtual ICollection<Deck> Decks { get; } = new List<Deck>();

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual ICollection<PlayedCard> PlayedCards { get; } = new List<PlayedCard>();
}
