﻿using System.ComponentModel.DataAnnotations.Schema;

namespace magix_api;

public partial class Deck
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int PlayerId { get; set; }
    public virtual Player? Player { get; set; }
    public required string Name { get; set; }
    public int HeroId { get; set; }
    public virtual Hero? Hero { get; set; }
    public int TalentId { get; set; }
    public virtual Talent? Talent { get; set; }
    public int FactionId { get; set; }
    public virtual Faction? Faction { get; set; }
    public bool Active { get; set; } = false;
    public virtual ICollection<Game>? Games { get; }
    public virtual ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
    [NotMapped]
    public List<Card>? Cards { get; set; }
}
