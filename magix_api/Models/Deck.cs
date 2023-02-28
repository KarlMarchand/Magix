﻿namespace magix_api;

public partial class Deck
{
    public int Id { get; set; }

    public Player Player { get; set; } = default!;

    public string Name { get; set; } = default!;

    public Hero Hero { get; set; } = default!;

    public Talent Talent { get; set; } = default!;

    public Faction Faction { get; set; } = default!;

    public bool Active { get; set; } = true;

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual ICollection<DeckCard> DeckCard { get; } = new List<DeckCard>();
}
